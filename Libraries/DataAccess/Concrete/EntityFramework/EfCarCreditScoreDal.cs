using Core.DataAccess.RepositoryPattern.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarCreditScoreDal : EfRepositoryBase<CarCreditScore, ReCapContext>, ICarCreditScoreDal
    {
    }
}