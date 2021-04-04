using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelTypesController : ControllerBase
    {
        private readonly IFuelTypeService _fuelTypeService;

        public FuelTypesController(IFuelTypeService gearTypeService)
        {
            _fuelTypeService = gearTypeService;
        }

        [HttpGet("GetViewList")]
        public async Task<IActionResult> GetDtoListAsync()
        {
            var result = await _fuelTypeService.GetAllViewDtos();
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
