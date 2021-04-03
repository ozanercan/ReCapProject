using Business.Abstract;
using Business.Constants;
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
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
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

            return new SuccessDataResult<User>(userToCreate, Messages.UserAdded);
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
