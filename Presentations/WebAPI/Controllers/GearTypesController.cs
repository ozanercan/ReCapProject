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
    public class GearTypesController : ControllerBase
    {
        private readonly IGearTypeService _gearTypeService;

        public GearTypesController(IGearTypeService gearTypeService)
        {
            _gearTypeService = gearTypeService;
        }

        [HttpGet("GetViewList")]
        public async Task<IActionResult> GetDtoListAsync()
        {
            var result = await _gearTypeService.GetAllViewDtos();
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
