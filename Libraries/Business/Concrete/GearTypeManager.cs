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
    public class GearTypeManager : IGearTypeService
    {
        private readonly IGearTypeDal _gearTypeDal;

        public GearTypeManager(IGearTypeDal gearTypeDal)
        {
            _gearTypeDal = gearTypeDal;
        }

        public async Task<IDataResult<List<GearTypeViewDto>>> GetAllViewDtos()
        {
            var gearTypes = await _gearTypeDal.GetAllNoTrackingAsync();
            if (gearTypes.Count == 0)
                return new ErrorDataResult<List<GearTypeViewDto>>(null, Messages.GearTypesNotFound);

            List<GearTypeViewDto> gearTypeDtos = new List<GearTypeViewDto>();

            gearTypes.ForEach(p =>
             {
                 gearTypeDtos.Add(new GearTypeViewDto() { Name = p.Name });
             });

            return new SuccessDataResult<List<GearTypeViewDto>>(gearTypeDtos, Messages.GearTypesListed);
        }

        public async Task<IDataResult<GearType>> GetByNameAsync(string name)
        {
            var result = await _gearTypeDal.GetAsync(p => p.Name.Equals(name));
            if (result == null)
                return new ErrorDataResult<GearType>(null, Messages.GearTypeNotFound);

            return new SuccessDataResult<GearType>(result, Messages.GearTypeBrought);
        }
    }
}
