using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        readonly IUserService _userService;
        readonly ITokenHelper _tokenHelper;
        readonly ICustomerService _customerService;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICustomerService customerService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _customerService = customerService;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user)
        {
            var claimResult = await _userService.GetClaimsAsync(user);
            if (!claimResult.Success)
                return new ErrorDataResult<AccessToken>(null, claimResult.Message);

            var createdAccessToken = _tokenHelper.CreateToken(user, claimResult.Data);

            return new SuccessDataResult<AccessToken>(createdAccessToken, Messages.AccessTokenCreated);
        }

        public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByMailAsync(userForLoginDto.Email);
            if (!userToCheck.Success)
                return new ErrorDataResult<User>(null, Messages.UserNotFound);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
                return new ErrorDataResult<User>(null, Messages.PasswordError);

            return new SuccessDataResult<User>(userToCheck.Data, Messages.LoginSuccess);
        }

        [ValidationAspect(typeof(UserForRegisterDtoValidator))]
        public async Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto)
        {
            var rulesResult = BusinessRules.Run((await this.UserExistAsync(userForRegisterDto.Email)));
            if (!rulesResult.Success)
                return new ErrorDataResult<User>(null, rulesResult.Message);


            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userToCreate = new User()
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            var userAddResult = await _userService.AddAsync(userToCreate);
            if (!userAddResult.Success)
                return new ErrorDataResult<User>(null, Messages.UserNotAdded);

            var customerAddResult = await _customerService.AddAsync(new CustomerAddDto()
            {
                CompanyName = userForRegisterDto.CompanyName,
                UserId = userToCreate.Id
            });
            if (!customerAddResult.Success)
                return new ErrorDataResult<User>(null, customerAddResult.Message);

            var authorizationResult = await AddDefaultAuthorizationAsync(userToCreate.Id);
            if (!authorizationResult.Success)
                return new ErrorDataResult<User>(null, authorizationResult.Message);

            return new SuccessDataResult<User>(userToCreate, Messages.UserAdded);
        }

        private async Task<IResult> AddDefaultAuthorizationAsync(int userId)
        {
            var userOperationClaimAddDto = new UserOperationClaimAddDto()
            {
                UserId = userId,
                OperationClaimId = 2
            };
            return await _userService.AddUserOperationClaimAsync(userOperationClaimAddDto);
        }

        public async Task<IResult> UserExistAsync(string email)
        {
            var userToCheck = await _userService.GetByMailAsync(email);
            if (userToCheck.Success)
                return new ErrorResult(Messages.UserAlreadyExist);

            return new SuccessResult(Messages.UserNotFound);
        }
    }
}
