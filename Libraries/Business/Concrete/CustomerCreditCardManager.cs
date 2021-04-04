using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerCreditCardManager : ICustomerCreditCardService
    {
        private readonly ICustomerCreditCardDal _customerCreditCardDal;

        public CustomerCreditCardManager(ICustomerCreditCardDal customerCreditCardDal)
        {
            _customerCreditCardDal = customerCreditCardDal;
        }

        [ValidationAspect(typeof(CustomerCreditCardAddDtoValidator))]
        [CacheRemoveAspect("ICustomerCreditCardService.Get")]
        [SecuredOperation("customer")]
        public async Task<IResult> AddAsync(CustomerCreditCardAddDto customerCreditCartAddDto)
        {
            CustomerCreditCard customerCreditCardToAdd = new CustomerCreditCard()
            {
                UserId = customerCreditCartAddDto.UserId,
                CardNumber = customerCreditCartAddDto.CardNumber,
                CardOwnerFullName = customerCreditCartAddDto.CardOwnerFullName,
                Cvv = customerCreditCartAddDto.Cvv,
                ExpiryDate = customerCreditCartAddDto.ExpiryDate
            };
            bool result = await _customerCreditCardDal.AddAsync(customerCreditCardToAdd);
            if (!result)
                return new ErrorResult(Messages.CustomerCreditCardNotAdded);

            return new SuccessResult(Messages.CustomerCreditCardAdded);
        }

        public async Task<IDataResult<List<CustomerCreditCardDto>>> GetCardsByCustomerIdAsync(int customerId)
        {
            var customerCreditCardsResult = await _customerCreditCardDal.GetAllNoTrackingAsync(p => p.UserId == customerId);
            if (customerCreditCardsResult.Count == 0)
                return new ErrorDataResult<List<CustomerCreditCardDto>>(null, Messages.CustomerCreditCardFound);

            var customerCreditCardDtoList = new List<CustomerCreditCardDto>();
            foreach (var creditCard in customerCreditCardsResult)
            {
                customerCreditCardDtoList.Add(new CustomerCreditCardDto()
                {
                    UserId = creditCard.UserId,
                    CardNumber = creditCard.CardNumber,
                    CardOwnerFullName = creditCard.CardOwnerFullName,
                    Cvv = creditCard.Cvv,
                    ExpiryDate = creditCard.ExpiryDate
                });
            }

            return new SuccessDataResult<List<CustomerCreditCardDto>>(customerCreditCardDtoList, Messages.CreditCardsListed);
        }
    }
}
