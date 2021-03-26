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
        public IActionResult Add(CustomerCreditCardAddDto customerCreditCardAddDto)
        {
            var addResult = _customerCreditCardService.Add(customerCreditCardAddDto);
            if (!addResult.Success)
                return BadRequest(addResult);

            return Ok(addResult);
        }

        [HttpGet("getcardsbycustomerid")]
        public IActionResult GetCardsByCustomerId(int customerId)
        {
            var addResult = _customerCreditCardService.GetCardsByCustomerId(customerId);
            if (!addResult.Success)
                return BadRequest(addResult);

            return Ok(addResult);
        }
    }
}
