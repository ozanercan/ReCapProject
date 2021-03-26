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

        [HttpPatch("updatewithuser")]
        public IActionResult UpdateWithUser(CustomerUpdateDto customerUpdateDto)
        {
            var result = _customerService.UpdateWithUser(customerUpdateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _customerService.GetById(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getdetailbyemail")]
        public IActionResult GetDetailByUserId(string email)
        {
            var result = _customerService.GetCustomerDetailByEmail(email);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getdetailcustomers")]
        public IActionResult GetDetailCustomers()
        {
            var result = _customerService.GetCustomerDetails();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CustomerAddDto customerCreateDto)
        {
            var result = _customerService.Add(customerCreateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Customer customer)
        {
            var result = _customerService.Update(customer);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Customer customer)
        {
            var result = _customerService.Delete(customer);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
