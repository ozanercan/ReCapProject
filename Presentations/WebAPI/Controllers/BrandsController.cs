using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _brandService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _brandService.GetAllAsync();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(BrandAddDto brandAddDto)
        {
            var result = await _brandService.AddAsync(brandAddDto);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync(BrandUpdateDto brandUpdateDto)
        {
            var result = await _brandService.UpdateAsync(brandUpdateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(Brand brand)
        {
            var result = await _brandService.DeleteAsync(brand);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
