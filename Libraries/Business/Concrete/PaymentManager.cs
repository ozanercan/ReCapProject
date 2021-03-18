using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly IPaymentDal _paymentDal;
        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        [ValidationAspect(typeof(PaymentAddDtoValidator))]
        public IResult Add(PaymentAddDto paymentAddDto)
        {
            Payment paymentToAdd = new Payment()
            {
                RentId = int.Parse(paymentAddDto.RentId),
                NameSurname = paymentAddDto.NameSurname,
                CardNumber = paymentAddDto.CardNumber,
                Cvv = paymentAddDto.Cvv,
                ExpiryDate = paymentAddDto.ExpiryDate
            };

            var addResult = _paymentDal.Add(paymentToAdd);
            if (!addResult)
                return new ErrorResult(Messages.PaymentCancelled);

            return new SuccessResult(Messages.PaymentSuccessful);
        }
    }
}
