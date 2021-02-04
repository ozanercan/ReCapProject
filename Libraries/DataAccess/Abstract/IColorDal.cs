using Core.DataAccess.RepositoryPattern.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IColorDal : IEfRepository<Color>
    {
    }
}