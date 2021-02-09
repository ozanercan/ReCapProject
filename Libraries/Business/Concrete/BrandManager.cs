using Business.Abstract;
using Business.Contants;
using Core.Business.Results.Abstract;
using Core.Business.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IBusinessResult Add(Brand brand)
        {
            bool addResult = _brandDal.Add(brand);

            string message;
            if (addResult == true)
                message = Messages.BrandAdded;
            else
                message = Messages.BrandNotAdded;

            return new BusinessResult(message, addResult);
        }

        public IBusinessResult Delete(Brand brand)
        {
            bool deleteResult = _brandDal.Delete(brand);

            string message;
            if (deleteResult == true)
                message = Messages.BrandDeleted;
            else
                message = Messages.BrandNotDeleted;

            return new BusinessResult(message, deleteResult);
        }

        public IBusinessResult DeleteById(int id)
        {
            var brandToDelete = GetById(id);
            return Delete(brandToDelete.Data);
        }

        public IBusinessDataResult<List<Brand>> GetAll()
        {
            var data = _brandDal.GetAll();

            string message;
            bool isSuccess;
            if (data == null || data.Count <= 0)
            {
                message = Messages.BrandNotFound;
                isSuccess = false;
            }
            else
            {
                message = Messages.BrandGetListByRegistered;
                isSuccess = true;
            }

            return new BusinessDataResult<List<Brand>>(message, isSuccess, data);
        }

        public IBusinessDataResult<Brand> GetById(int id)
        {
            var data = _brandDal.GetById(id);

            string message;
            bool isSuccess;
            if (data == null)
            {
                message = Messages.BrandNotFound;
                isSuccess = false;
            }
            else
            {
                message = Messages.BrandGetListByRegistered;
                isSuccess = true;
            }

            return new BusinessDataResult<Brand>(message, isSuccess, data);
        }

        public IBusinessResult Update(Brand brand)
        {
            bool updateResult = _brandDal.Update(brand);

            string message;
            if (updateResult == true)
                message = Messages.BrandUpdated;
            else
                message = Messages.BrandNotUpdated;

            return new BusinessResult(message, updateResult);
        }

        public IBusinessResult Update(int id, Brand newBrand)
        {
            var findedCarResult = GetById(id);

            Brand updatedBrand = InputToCar(findedCarResult.Data, newBrand);

            return Update(updatedBrand);
        }

        private Brand InputToCar(Brand oldBrand, Brand newBrand)
        {
            oldBrand.Name = newBrand.Name;
            return oldBrand;
        }
    }
}