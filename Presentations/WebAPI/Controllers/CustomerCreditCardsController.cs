using Business.Abstract;
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
    public class CustomerCreditCardsController : ControllerBase
    {
        private readonly ICustomerCreditCardService _customerCreditCardService;

        public CustomerCreditCardsController(ICustomerCreditCardService customerCreditCardService)
        {
            _customerCreditCardService = customerCreditCardService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(CustomerCreditCardAddDto customerCreditCardAddDto)
        {
            var addResult = await _customerCreditCardService.AddAsync(customerCreditCardAddDto);
            if (!addResult.Success)
                return BadRequest(addResult);

            return Ok(addResult);
        }

        [HttpGet("getcardsbycustomerid")]
        public async Task<IActionResult> GetCardsByCustomerIdAsync(int customerId)
        {
            var addResult = await _customerCreditCardService.GetCardsByCustomerIdAsync(customerId);
            if (!addResult.Success)
                return BadRequest(addResult);

            return Ok(addResult);
        }
    }
}
