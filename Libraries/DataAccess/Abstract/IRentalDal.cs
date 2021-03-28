using Core.DataAccess.RepositoryPattern.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEfRepository<Rental>
    {
        public Task<List<RentalDto>> GetRentalDtosAsync();
    }
}
