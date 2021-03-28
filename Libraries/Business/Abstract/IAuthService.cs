using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto);

        Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto);
        
        Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user);

        /// <summary>
        /// Mail adresine göre kullanıcının olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Kullanıcı varsa ErrorResult, kullanıcı yoksa SuccessResult döner.</returns>
        Task<IResult> UserExistAsync(string email);
    }
}
