using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetById(int id);

        IDataResult<User> GetLastInsertUser();

        IDataResult<List<User>> GetAll();

        IResult Add(UserCreateDto userCreateDto);

        IResult Update(User user);

        IResult Update(int id, User newUser);

        IResult Delete(User brand);

        IResult DeleteById(int id);
    }
}