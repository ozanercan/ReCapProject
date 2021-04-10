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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPatch("UpdateWithUser")]
        public async Task<IActionResult> UpdateWithUserAsync(CustomerUpdateDto customerUpdateDto)
        {
            var result = await _customerService.UpdateWithUserAsync(customerUpdateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _customerService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetDetailByEmail")]
        public async Task<IActionResult> GetDetailByUserIdAsync(string email)
        {
            var result = await _customerService.GetCustomerDetailByEmailAsync(email);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetDetailCustomers")]
        public async Task<IActionResult> GetDetailCustomersAsync()
        {
            var result = await _customerService.GetCustomerDetailsAsync();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _customerService.GetAllAsync();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(CustomerAddDto customerCreateDto)
        {
            var result = await _customerService.AddAsync(customerCreateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(Customer customer)
        {
            var result = await _customerService.UpdateAsync(customer);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(Customer customer)
        {
            var result = await _customerService.DeleteAsync(customer);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
