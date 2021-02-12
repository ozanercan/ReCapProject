﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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

        public IResult Add(CustomerCreateDto customerCreateDto)
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
                return new ErrorResult(Messages.CustomerAdded);
        }

        public IResult Delete(Customer customer)
        {
            bool deleteResult = _customerDal.Delete(customer);

            if (deleteResult == true)
                return new SuccessResult(Messages.CustomerDeleted);
            else
                return new ErrorResult(Messages.CustomerDeleted);
        }

        public IResult DeleteById(int id)
        {
            var getResult = GetById(id);
            if (!getResult.Success)
                return getResult;

            return Delete(getResult.Data);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            var data = _customerDal.GetAll();

            if (data == null || data.Count <= 0)
                return new ErrorDataResult<List<Customer>>(data, Messages.CustomerNotFound);
            else
                return new SuccessDataResult<List<Customer>>(data, Messages.CustomerGetListByRegistered);
        }

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

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
        }

        public IResult Update(Customer customer)
        {
            bool updateResult = _customerDal.Update(customer);

            if (updateResult == true)
                return new SuccessResult(Messages.CustomerUpdated);
            else
                return new ErrorResult(Messages.CustomerNotUpdated);
        }

        public IResult Update(int id, Customer newCustomer)
        {
            var findedCustomerResult = GetById(id);

            if (!findedCustomerResult.Success)
                return findedCustomerResult;


            Customer updatedEntity = InputToCar(findedCustomerResult.Data, newCustomer);

            return Update(updatedEntity);
        }

        private Customer InputToCar(Customer oldCustomer, Customer newCustomer)
        {
            oldCustomer.CompanyName = newCustomer.CompanyName;

            return oldCustomer;
        }
    }
}