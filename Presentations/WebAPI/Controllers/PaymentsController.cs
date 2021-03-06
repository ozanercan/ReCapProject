﻿using Business.Abstract;
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
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(PaymentAddDto paymentAddDto)
        {
            var result = await _paymentService.AddAsync(paymentAddDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("IsCanPaymentByRentalId")]
        public async Task<IActionResult> IsCanPaymentByRentalIdAsync(string rentalId)
        {
            var result = await _paymentService.IsCanPaymentAsync(rentalId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
