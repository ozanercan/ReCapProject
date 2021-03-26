using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IUserService _userService;

        public CustomerManager(ICustomerDal customerDal, IUserService userService)
        {
            _customerDal = customerDal;
            _userService = userService;
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Add(CustomerAddDto customerCreateDto)
        {
            var lastUserInsertIdResult = _userService.GetLastInsertUser();

            if (!lastUserInsertIdResult.Success)
                return lastUserInsertIdResult;

            var createToCustomer = new Customer()
            {
                CompanyName = customerCreateDto.CompanyName,
                UserId = lastUserInsertIdResult.Data.Id
            };

            bool addResult = _customerDal.Add(createToCustomer);

            if (addResult == true)
                return new SuccessResult(Messages.CustomerAdded);
            else
                return new ErrorResult(Messages.CustomerNotAdded);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Delete(Customer customer)
        {
            bool deleteResult = _customerDal.Delete(customer);

            if (deleteResult == true)
                return new SuccessResult(Messages.CustomerDeleted);
            else
                return new ErrorResult(Messages.CustomerNotDeleted);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult DeleteById(int id)
        {
            var getResult = GetById(id);
            if (!getResult.Success)
                return getResult;

            return Delete(getResult.Data);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            var data = _customerDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Customer>>(data, Messages.CustomerNotFound);
            else
                return new SuccessDataResult<List<Customer>>(data, Messages.CustomerGetListByRegistered);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<Customer> GetById(int id)
        {
            var getResult = this.GetById(id);

            if (!getResult.Success)
                return new ErrorDataResult<Customer>(getResult.Data, Messages.CustomerNotFound);

            if (getResult == null)
                return new ErrorDataResult<Customer>(getResult.Data, Messages.CustomerNotFound);
            else
                return new SuccessDataResult<Customer>(getResult.Data, Messages.CustomerGetListByRegistered);
        }

        [PerformanceAspect(5)]
        //[CacheAspect]
        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
        }

        public IDataResult<CustomerDetailDto> GetCustomerDetailByEmail(string email)
        {
            var userResult = _userService.GetByMail(email);
            if (!userResult.Success)
                return new ErrorDataResult<CustomerDetailDto>(null, userResult.Message);

            var customer = _customerDal.Get(p => p.UserId == userResult.Data.Id);
            if (customer == null)
                return new ErrorDataResult<CustomerDetailDto>(null, Messages.CustomerNotFound);

            CustomerDetailDto customerDetailDto = new CustomerDetailDto()
            {
                Id = userResult.Data.Id,
                CompanyName = customer.CompanyName,
                Email = userResult.Data.Email,
                FirstName = userResult.Data.FirstName,
                LastName = userResult.Data.LastName,
                Status = userResult.Data.Status ? "Aktif" : "Pasif"
            };
            return new SuccessDataResult<CustomerDetailDto>(customerDetailDto, Messages.CustomerDetailBroughth);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer)
        {
            bool updateResult = _customerDal.Update(customer);

            if (updateResult == true)
                return new SuccessResult(Messages.CustomerUpdated);
            else
                return new ErrorResult(Messages.CustomerNotUpdated);
        }

        public IResult UpdateWithUser(CustomerUpdateDto customerUpdateDto)
        {
            var userResult = _userService.GetById(customerUpdateDto.Id);
            if (!userResult.Success)
                return new ErrorResult(userResult.Message);

            if (!HashingHelper.VerifyPasswordHash(customerUpdateDto.ActivePassword, userResult.Data.PasswordHash, userResult.Data.PasswordSalt))
                return new ErrorResult(Messages.PasswordError);

            var customerResult = _customerDal.Get(p => p.UserId == customerUpdateDto.Id);
            if (customerResult == null)
                return new ErrorResult(Messages.CustomerNotFound);

            customerResult.CompanyName = customerUpdateDto.CompanyName;

            userResult.Data.FirstName = customerUpdateDto.FirstName;
            userResult.Data.LastName = customerUpdateDto.LastName;
            userResult.Data.Email = customerUpdateDto.Email;

            if (customerUpdateDto.NewPassword.Length > 5)
            {
                HashingHelper.CreatePasswordHash(customerUpdateDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                userResult.Data.PasswordHash = passwordHash;
                userResult.Data.PasswordSalt = passwordSalt;
            }

            var customerUpdateResult = _customerDal.Update(customerResult);
            if (!customerUpdateResult)
                return new ErrorResult(Messages.CustomerNotUpdated);

            var userUpdateResult = _userService.Update(userResult.Data);
            if (!userUpdateResult.Success)
                return new ErrorResult(Messages.UserNotUpdated);

            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
