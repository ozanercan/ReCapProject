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
                RentalId = int.Parse(paymentAddDto.RentalId),
                MoneyPaid = paymentAddDto.MoneyPaid
            };

            var addResult = _paymentDal.Add(paymentToAdd);
            if (!addResult)
                return new ErrorResult(Messages.PaymentCancelled);

            return new SuccessResult(Messages.PaymentSuccessful);
        }
    }
}
