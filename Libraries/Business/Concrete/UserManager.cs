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
using System.Threading.Tasks;

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
        public async Task<IResult> AddAsync(User user)
        {
            bool addResult = await _userDal.AddAsync(user);

            if (addResult)
                return new SuccessResult(Messages.UserAdded);
            else
                return new ErrorResult(Messages.UserNotAdded);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> DeleteAsync(User brand)
        {
            bool deleteResult = await _userDal.DeleteAsync(brand);

            if (deleteResult)
                return new SuccessResult(Messages.UserDeleted);
            else
                return new ErrorResult(Messages.UserNotDeleted);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> DeleteByIdAsync(int id)
        {
            var getResult = await GetByIdAsync(id);
            if (!getResult.Success)
                return getResult;

            return await DeleteAsync(getResult.Data);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public async Task<IDataResult<List<User>>> GetAllAsync()
        {
            var data = await _userDal.GetAllAsync();

            if (data.Count == 0)
                return new ErrorDataResult<List<User>>(data, Messages.UserNotFound);
            else
                return new SuccessDataResult<List<User>>(data, Messages.UserGetListByRegistered);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public async Task<IDataResult<User>> GetByIdAsync(int id)
        {
            var getResult = await _userDal.GetAsync(p => p.Id == id);

            if (getResult == null)
                return new ErrorDataResult<User>(null, Messages.UserNotFound);

            return new SuccessDataResult<User>(getResult, Messages.UserGet);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public async Task<IDataResult<User>> GetByMailAsync(string mail)
        {
            var user = await _userDal.GetAsync(p => p.Email.Equals(mail));
            if (user == null)
                return new ErrorDataResult<User>(null, Messages.UserNotFound);

            return new SuccessDataResult<User>(user, Messages.UserAlreadyExist);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public async Task<IDataResult<List<OperationClaim>>> GetClaimsAsync(User user)
        {
            var operationClaims = await _userDal.GetClaimsAsync(user);
            if (operationClaims == null)
                return new ErrorDataResult<List<OperationClaim>>(null, Messages.ClaimsNotFound);

            return new SuccessDataResult<List<OperationClaim>>(operationClaims, Messages.ClaimsListed);
        }

        public async Task<IDataResult<UserFirstLastNameDto>> GetFirstNameLastNameByMailAsync(string mail)
        {
            var user = await _userDal.GetAsync(p => p.Email.Equals(mail));
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
        public async Task<IDataResult<User>> GetLastInsertUserAsync()
        {
            User user = (await _userDal.GetAllAsync()).Last();
            return new SuccessDataResult<User>(user);
        }

        public async Task<IResult> UpdateAsync(User user)
        {
            bool updateResult = await _userDal.UpdateAsync(user);
            
            if (!updateResult)
                return new ErrorResult(Messages.UserNotUpdated);

            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
