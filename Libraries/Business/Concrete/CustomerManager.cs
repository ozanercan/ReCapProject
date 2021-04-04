using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IResult> AddAsync(CustomerAddDto customerCreateDto)
        {
            var lastUserInsertIdResult = await _userService.GetLastInsertUserAsync();

            if (!lastUserInsertIdResult.Success)
                return lastUserInsertIdResult;

            var createToCustomer = new Customer()
            {
                CompanyName = customerCreateDto.CompanyName,
                UserId = lastUserInsertIdResult.Data.Id
            };

            bool addResult = await _customerDal.AddAsync(createToCustomer);

            if (addResult == true)
                return new SuccessResult(Messages.CustomerAdded);
            else
                return new ErrorResult(Messages.CustomerNotAdded);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(Customer customer)
        {
            bool deleteResult = await _customerDal.DeleteAsync(customer);

            if (deleteResult == true)
                return new SuccessResult(Messages.CustomerDeleted);
            else
                return new ErrorResult(Messages.CustomerNotDeleted);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        [SecuredOperation("admin")]
        public async Task<IResult> DeleteByIdAsync(int id)
        {
            var getResult = await GetByIdAsync(id);
            if (!getResult.Success)
                return getResult;

            return await DeleteAsync(getResult.Data);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<Customer>>> GetAllAsync()
        {
            var data = await _customerDal.GetAllAsync();

            if (data.Count == 0)
                return new ErrorDataResult<List<Customer>>(data, Messages.CustomerNotFound);
            else
                return new SuccessDataResult<List<Customer>>(data, Messages.CustomerGetListByRegistered);
        }

        [PerformanceAspect(5)]
        public async Task<IDataResult<Customer>> GetByIdAsync(int id)
        {
            var getResult = await _customerDal.GetAsync(p => p.UserId == id);

            if (getResult == null)
                return new ErrorDataResult<Customer>(null, Messages.CustomerNotFound);
            else
                return new SuccessDataResult<Customer>(getResult, Messages.CustomerGetListByRegistered);
        }

        [PerformanceAspect(5)]
        [CacheAspect]
        public async Task<IDataResult<List<CustomerDetailDto>>> GetCustomerDetailsAsync()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(await _customerDal.GetCustomerDetailsAsync());
        }

        public async Task<IDataResult<CustomerDetailDto>> GetCustomerDetailByEmailAsync(string email)
        {
            var userResult = await _userService.GetByMailAsync(email);
            if (!userResult.Success)
                return new ErrorDataResult<CustomerDetailDto>(null, userResult.Message);

            var customer = await _customerDal.GetAsync(p => p.UserId == userResult.Data.Id);
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
        public async Task<IResult> UpdateAsync(Customer customer)
        {
            bool updateResult = await _customerDal.UpdateAsync(customer);

            if (updateResult == true)
                return new SuccessResult(Messages.CustomerUpdated);
            else
                return new ErrorResult(Messages.CustomerNotUpdated);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public async Task<IResult> UpdateWithUserAsync(CustomerUpdateDto customerUpdateDto)
        {
            var userResult = await _userService.GetByIdAsync(customerUpdateDto.Id);
            if (!userResult.Success)
                return new ErrorResult(userResult.Message);

            if (!HashingHelper.VerifyPasswordHash(customerUpdateDto.ActivePassword, userResult.Data.PasswordHash, userResult.Data.PasswordSalt))
                return new ErrorResult(Messages.PasswordError);

            var customerResult = await _customerDal.GetAsync(p => p.UserId == customerUpdateDto.Id);
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

            var customerUpdateResult = await _customerDal.UpdateAsync(customerResult);
            if (!customerUpdateResult)
                return new ErrorResult(Messages.CustomerNotUpdated);

            var userUpdateResult = await _userService.UpdateAsync(userResult.Data);
            if (!userUpdateResult.Success)
                return new ErrorResult(Messages.UserNotUpdated);

            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
