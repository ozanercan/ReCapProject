using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
    public class PaymentManager : IPaymentService
    {
        private readonly IPaymentDal _paymentDal;
        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        [ValidationAspect(typeof(PaymentAddDtoValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        [SecuredOperation("customer")]
        public async Task<IResult> AddAsync(PaymentAddDto paymentAddDto)
        {
            var rulesResult=BusinessRules.Run(await this.CheckIfPaymentHasBeenMadeByRentalId(paymentAddDto.RentalId));
            if (!rulesResult.Success)
                return rulesResult;

            Payment paymentToAdd = new Payment()
            {
                RentalId = int.Parse(paymentAddDto.RentalId),
                MoneyPaid = paymentAddDto.MoneyPaid
            };

            var addResult = await _paymentDal.AddAsync(paymentToAdd);
            if (!addResult)
                return new ErrorResult(Messages.PaymentCancelled);

            return new SuccessResult(Messages.PaymentSuccessful);
        }

        public async Task<IResult> IsCanPaymentAsync(string rentalId)
        {
            return await this.CheckIfPaymentHasBeenMadeByRentalId(rentalId);
        }

        private async Task<IResult> CheckIfPaymentHasBeenMadeByRentalId(string rentalId)
        {
            var findedPayment = await _paymentDal.GetNoTrackingAsync(p => p.RentalId == Convert.ToInt32(rentalId));
            if (findedPayment != null)
                return new ErrorResult(Messages.PaymentAlreadyMade);

            return new SuccessResult(Messages.PaymentHasNotBeen);
        }
    }
}
