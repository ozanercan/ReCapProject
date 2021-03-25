using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getcarrentpricebyrentalid")]
        public IActionResult GetCarRentPriceByRentalId(int rentalId)
        {
            var result = _carService.GetMoneyToPaidByRentalId(rentalId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getdetails")]
        public IActionResult GetAllDto()
        {
            var result = _carService.GetCarDetails();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcardetailsbybrandid")]
        public IActionResult GetCarDetailsByBrandId(int brandId)
        {
            var result = _carService.GetCarDetailsByBrandId(brandId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getcardetailsbybrandname")]
        public IActionResult GetCarDetailsByBrandName(string brandName)
        {
            var result = _carService.GetCarDetailsByBrandName(brandName);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getcardetailsbycolorid")]
        public IActionResult GetCarDetailsByColorId(int colorId)
        {
            var result = _carService.GetCarDetailsByColorId(colorId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getcardetailsbycolorname")]
        public IActionResult GetCarDetailsByColorName(string colorName)
        {
            var result = _carService.GetCarDetailsByColorName(colorName);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getcardetailsbycarid")]
        public IActionResult GetCarDetailsById(int carId)
        {
            var result = _carService.GetCarDetailById(carId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("getcardetailsbyfilters")]
        public IActionResult GetCarDetailsByFilters(CarFilterDto carFilterDto)
        {
            var result = _carService.GetCarDetailsByFilters(carFilterDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getrentalcars")]
        public IActionResult GetRentalCars()
        {
            var result = _carService.GetRentalCars();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CarAddDto carAddDto)
        {
            var result = _carService.Add(carAddDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPatch("update")]
        public IActionResult Update(CarUpdateDto carUpdateDto)
        {
            var result = _carService.Update(carUpdateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
