using Core.DataAccess.RepositoryPattern.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEfRepository<User>
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);
    }
}
