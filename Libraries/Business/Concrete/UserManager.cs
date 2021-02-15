using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(UserCreateDto userCreateDto)
        {
            var userToCreate = new User()
            {
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Email = userCreateDto.Email,
                Password = userCreateDto.Password
            };

            bool addResult = _userDal.Add(userToCreate);

            if (addResult == true)
                return new SuccessResult(Messages.UserAdded);
            else
                return new ErrorResult(Messages.UserNotAdded);
        }

        public IResult Delete(User brand)
        {
            bool deleteResult = _userDal.Delete(brand);

            if (deleteResult == true)
                return new SuccessResult(Messages.UserDeleted);
            else
                return new ErrorResult(Messages.UserNotDeleted);
        }

        public IResult DeleteById(int id)
        {
            var getResult = GetById(id);
            if (!getResult.Success)
                return getResult;

            return Delete(getResult.Data);
        }

        public IDataResult<List<User>> GetAll()
        {
            var data = _userDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<User>>(data, Messages.UserNotFound);
            else
                return new SuccessDataResult<List<User>>(data, Messages.UserGetListByRegistered);
        }

        public IDataResult<User> GetById(int id)
        {
            var getResult = this.GetById(id);

            if (!getResult.Success)
                return new ErrorDataResult<User>(getResult.Data, Messages.UserNotFound);

            if (getResult == null)
                return new ErrorDataResult<User>(getResult.Data, Messages.UserNotFound);
            else
                return new SuccessDataResult<User>(getResult.Data, Messages.UserGetListByRegistered);
        }

        public IDataResult<User> GetLastInsertUser()
        {
            User user = _userDal.GetAll().Last();
            return new SuccessDataResult<User>(user);
        }

        public IResult Update(User brand)
        {
            bool updateResult = _userDal.Update(brand);

            if (updateResult == true)
                return new SuccessResult(Messages.UserUpdated);
            else
                return new ErrorResult(Messages.UserNotUpdated);
        }

        public IResult Update(int id, User newBrand)
        {
            var findedEntityResult = GetById(id);

            if (!findedEntityResult.Success)
                return findedEntityResult;


            User userToUpdate = InputToCar(findedEntityResult.Data, newBrand);

            return Update(userToUpdate);
        }

        private User InputToCar(User oldUser, User newUser)
        {
            oldUser.FirstName = newUser.FirstName;
            oldUser.LastName = newUser.LastName;
            oldUser.Email = newUser.Email;
            oldUser.Password = newUser.Password;

            return oldUser;
        }
    }
}
