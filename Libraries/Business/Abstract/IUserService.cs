using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<OperationClaim>>> GetClaimsAsync(User user);

        /// <summary>
        /// Mail adresine göre kullanıcının olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Kullanıcı varsa SuccessResult, kullanıcı yoksa ErrorResult döner.</returns>
        Task<IDataResult<User>> GetByMailAsync(string mail);

        Task<IDataResult<UserFirstLastNameDto>> GetFirstNameLastNameByMailAsync(string mail);

        Task<IResult> AddUserOperationClaimAsync(UserOperationClaimAddDto userOperationClaimAddDto);

        Task<IDataResult<User>> GetByIdAsync(int id);

        Task<IDataResult<User>> GetLastInsertUserAsync();

        Task<IDataResult<List<User>>> GetAllAsync();

        Task<IResult> AddAsync(User user);

        Task<IResult> UpdateAsync(User user);

        Task<IResult> DeleteAsync(User brand);

        Task<IResult> DeleteByIdAsync(int id);
    }
}