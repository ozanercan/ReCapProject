using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);

        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        
        IDataResult<AccessToken> CreateAccessToken(User user);

        /// <summary>
        /// Mail adresine göre kullanıcının olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Kullanıcı varsa ErrorResult, kullanıcı yoksa SuccessResult döner.</returns>
        IResult UserExist(string email);
    }
}
