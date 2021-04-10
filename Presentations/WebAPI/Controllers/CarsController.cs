using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _carService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("CalculateTotalPrice")]
        public async Task<IActionResult> CalculateTotalPriceAsync(CarPriceCalculateDto carPriceCalculateDto)
        {
            var result = await _carService.GetCalculateTotalPrice(carPriceCalculateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCarUpdateDtoById")]
        public async Task<IActionResult> GetCarUpdateDtoByIdAsync(int id)
        {
            var result = await _carService.GetUpdateDtoByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCarRentPriceByRentalId")]
        public async Task<IActionResult> GetCarRentPriceByRentalIdAsync(int rentalId)
        {
            var result = await _carService.GetMoneyToPaidByRentalIdAsync(rentalId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetDetails")]
        public async Task<IActionResult> GetAllDtoAsync()
        {
            var result = await _carService.GetCarDetailsAsync();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetCarDetailsByBrandId")]
        public async Task<IActionResult> GetCarDetailsByBrandIdAsync(int brandId)
        {
            var result = await _carService.GetCarDetailsByBrandIdAsync(brandId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCarDdetailsByBrandName")]
        public async Task<IActionResult> GetCarDetailsByBrandNameAsync(string brandName)
        {
            var result = await _carService.GetCarDetailsByBrandNameAsync(brandName);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCarDetailsByColorId")]
        public async Task<IActionResult> GetCarDetailsByColorIdAsync(int colorId)
        {
            var result = await _carService.GetCarDetailsByColorIdAsync(colorId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCarDetailsByColorName")]
        public async Task<IActionResult> GetCarDetailsByColorNameAsync(string colorName)
        {
            var result = await _carService.GetCarDetailsByColorNameAsync(colorName);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCarDetailsByCarId")]
        public async Task<IActionResult> GetCarDetailsByIdAsync(int carId)
        {
            var result = await _carService.GetCarDetailByIdAsync(carId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("GetCarDetailsByFilters")]
        public async Task<IActionResult> GetCarDetailsByFiltersAsync(CarFilterDto carFilterDto)
        {
            var result = await _carService.GetCarDetailsByFiltersAsync(carFilterDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _carService.GetAllAsync();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getrentalcars")]
        public async Task<IActionResult> GetRentalCarsAsync()
        {
            var result = await _carService.GetRentalCarsAsync();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(CarAddDto carAddDto)
        {
            var result = await _carService.AddAsync(carAddDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> UpdateAsync(CarUpdateDto carUpdateDto)
        {
            var result = await _carService.UpdateAsync(carUpdateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(Car car)
        {
            var result = await _carService.DeleteAsync(car);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
