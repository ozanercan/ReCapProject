using Core.DataAccess.RepositoryPattern.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEfRepository<Rental>
    {
        public List<RentalDto> GetRentalDtos();
    }
}
