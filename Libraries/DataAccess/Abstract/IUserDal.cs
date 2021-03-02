using Core.DataAccess.RepositoryPattern.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEfRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
