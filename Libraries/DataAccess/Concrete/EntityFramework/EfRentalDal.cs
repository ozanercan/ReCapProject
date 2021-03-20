using Core.DataAccess.RepositoryPattern.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<RentalDto> GetRentalDtos()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var query = from rental in context.Rentals
                            join user in context.Users
                            on rental.CustomerId equals user.Id
                            join car in context.Cars
                            on rental.CarId equals car.Id
                            join brand in context.Brands
                            on car.BrandId equals brand.Id
                            join payment in context.Payments
                            on rental.Id equals payment.RentalId into lj
                            from paymentJoin in lj.DefaultIfEmpty()
                            select new RentalDto
                            {
                                Id = rental.Id,
                                BrandName = brand.Name,
                                Customer = string.Join(" ", user.FirstName, user.LastName),
                                RentDate = rental.RentDate,
                                ReturnDate = rental.ReturnDate,
                                Price = paymentJoin.MoneyPaid
                            };

                return query.ToList();
            }
        }
    }
}
