using Core.DataAccess.RepositoryPattern.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfRepositoryBase<Customer, ReCapContext>, ICustomerDal
    {
        public async Task<List<CustomerDetailDto>> GetCustomerDetailsAsync()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from user in context.Users
                             join customer in context.Customers
                             on user.Id equals customer.UserId
                             select new CustomerDetailDto
                             {
                                 Id = user.Id,
                                 CompanyName = customer.CompanyName,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 Status = user.Status ? "Aktif" : "Pasif"
                             };

                return await result.ToListAsync();
            }
        }
    }
}
