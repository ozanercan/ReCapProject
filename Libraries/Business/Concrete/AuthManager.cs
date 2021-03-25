using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        readonly IUserService _userService;
        ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claimResult = _userService.GetClaims(user);
            if (!claimResult.Success)
                return new ErrorDataResult<AccessToken>(null, claimResult.Message);

            var createdAccessToken = _tokenHelper.CreateToken(user, claimResult.Data);

            return new SuccessDataResult<AccessToken>(createdAccessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (!userToCheck.Success)
                return new ErrorDataResult<User>(null, Messages.UserNotFound);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
                return new ErrorDataResult<User>(null, Messages.PasswordError);

            return new SuccessDataResult<User>(userToCheck.Data, Messages.LoginSuccess);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var rulesResult = BusinessRules.Run(this.UserExist(userForRegisterDto.Email));
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

            var userAddResult = _userService.Add(userToCreate);
            if (!userAddResult.Success)
                return new ErrorDataResult<User>(null, Messages.UserNotAdded);

            return new SuccessDataResult<User>(userToCreate, Messages.UserAdded);
        }

       
        public IResult UserExist(string email)
        {
            var userToCheck = _userService.GetByMail(email);
            if (userToCheck.Success)
                return new ErrorResult(Messages.UserAlreadyExist);

            return new SuccessResult(Messages.UserNotFound);
        }
    }
}
