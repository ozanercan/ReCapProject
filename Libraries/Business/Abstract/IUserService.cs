using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);

        /// <summary>
        /// Mail adresine göre kullanıcının olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Kullanıcı varsa SuccessResult, kullanıcı yoksa ErrorResult döner.</returns>
        IDataResult<User> GetByMail(string mail);

        IDataResult<UserFirstLastNameDto> GetFirstNameLastNameByMail(string mail);

        IDataResult<User> GetById(int id);

        IDataResult<User> GetLastInsertUser();

        IDataResult<List<User>> GetAll();

        IResult Add(User user);

        IResult Update(User user);


        IResult Delete(User brand);

        IResult DeleteById(int id);
    }
}