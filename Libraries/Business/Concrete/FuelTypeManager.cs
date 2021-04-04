using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FuelTypeManager : IFuelTypeService
    {
        private readonly IFuelTypeDal _fuelTypeDal;

        public FuelTypeManager(IFuelTypeDal fuelTypeDal)
        {
            _fuelTypeDal = fuelTypeDal;
        }

        public async Task<IDataResult<List<FuelTypeViewDto>>> GetAllViewDtos()
        {
            var fuelTypes = await _fuelTypeDal.GetAllNoTrackingAsync();
            if (fuelTypes.Count == 0)
                return new ErrorDataResult<List<FuelTypeViewDto>>(null, Messages.FuelTypesNotFound);

            List<FuelTypeViewDto> fuelTypeDtos = new List<FuelTypeViewDto>();

            fuelTypes.ForEach(p =>
            {
                fuelTypeDtos.Add(new FuelTypeViewDto() { Name = p.Name });
            });

            return new SuccessDataResult<List<FuelTypeViewDto>>(fuelTypeDtos, Messages.FuelTypesListed);
        }

        public async Task<IDataResult<FuelType>> GetByNameAsync(string name)
        {
            var result = await _fuelTypeDal.GetAsync(p => p.Name.Equals(name));
            if (result == null)
                return new ErrorDataResult<FuelType>(null, Messages.FuelTypeNotFound);

            return new SuccessDataResult<FuelType>(result, Messages.FuelTypeBrought);
        }
    }
}
