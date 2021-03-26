using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using System;

namespace Business.Concrete
{
    public class CustomerCreditScoreManager : ICustomerCreditScoreService
    {
        private Random _random;
        public CustomerCreditScoreManager()
        {
            _random = new Random();
        }
        public IDataResult<int> CalculateByCustomerId(int userId)
        {
            return new SuccessDataResult<int>(_random.Next(0, 1900), Messages.CreditScoreCalculated);
        }
    }
}
