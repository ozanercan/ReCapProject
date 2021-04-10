using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
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
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _rentalService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetCustomerIdById")]
        public async Task<IActionResult> GetCustomerIdByIdAsync(int id)
        {
            var result = await _rentalService.GetCustomerIdByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _rentalService.GetAllAsync();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetDetails")]
        public async Task<IActionResult> GetAllDtoAsync()
        {
            var result = await _rentalService.GetAllDtoAsync();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(RentalAddDto rentalCreateDto)
        {
            var result = await _rentalService.AddAsync(rentalCreateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(Rental rental)
        {
            var result = await _rentalService.UpdateAsync(rental);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(Rental rental)
        {
            var result = await _rentalService.DeleteAsync(rental);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
