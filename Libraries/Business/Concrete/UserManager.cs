using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Entities.Concrete;
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

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            bool addResult = _userDal.Add(user);

            if (addResult == true)
                return new SuccessResult(Messages.UserAdded);
            else
                return new ErrorResult(Messages.UserNotAdded);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User brand)
        {
            bool deleteResult = _userDal.Delete(brand);

            if (deleteResult == true)
                return new SuccessResult(Messages.UserDeleted);
            else
                return new ErrorResult(Messages.UserNotDeleted);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult DeleteById(int id)
        {
            var getResult = GetById(id);
            if (!getResult.Success)
                return getResult;

            return Delete(getResult.Data);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<User>> GetAll()
        {
            var data = _userDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<User>>(data, Messages.UserNotFound);
            else
                return new SuccessDataResult<List<User>>(data, Messages.UserGetListByRegistered);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<User> GetById(int id)
        {
            var getResult = _userDal.Get(p => p.Id == id);

            if (getResult == null)
                return new ErrorDataResult<User>(null, Messages.UserNotFound);

            return new SuccessDataResult<User>(getResult, Messages.UserGet);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<User> GetByMail(string mail)
        {
            var user = _userDal.Get(p => p.Email.Equals(mail));
            if (user == null)
                return new ErrorDataResult<User>(null, Messages.UserNotFound);

            return new SuccessDataResult<User>(user, Messages.UserAlreadyExist);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var operationClaims = _userDal.GetClaims(user);
            if (operationClaims == null)
                return new ErrorDataResult<List<OperationClaim>>(null, Messages.ClaimsNotFound);

            return new SuccessDataResult<List<OperationClaim>>(operationClaims, Messages.ClaimsListed);
        }

        public IDataResult<UserFirstLastNameDto> GetFirstNameLastNameByMail(string mail)
        {
            var user = _userDal.Get(p => p.Email.Equals(mail));
            if (user == null)
                return new ErrorDataResult<UserFirstLastNameDto>(null, Messages.UserNotFound);

            UserFirstLastNameDto userFirstLastNameDto = new UserFirstLastNameDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return new SuccessDataResult<UserFirstLastNameDto>(userFirstLastNameDto, Messages.UserGet);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<User> GetLastInsertUser()
        {
            User user = _userDal.GetAll().Last();
            return new SuccessDataResult<User>(user);
        }

        public IResult Update(User user)
        {
            bool updateResult = _userDal.Update(user);

            if (!updateResult)
                return new ErrorResult(Messages.UserNotUpdated);

            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
