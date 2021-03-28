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
    public class CarCreditScoresController : ControllerBase
    {
        private readonly ICarCreditScoreService _carCreditScoreService;

        public CarCreditScoresController(ICarCreditScoreService carCreditScoreService)
        {
            _carCreditScoreService = carCreditScoreService;
        }

        [HttpGet("getminscorebycarid")]
        public async Task<IActionResult> GetMinScoreByCarIdAsync(int carId)
        {
            var getResult = await _carCreditScoreService.GetMinScoreByCarIdAsync(carId);
            if (!getResult.Success)
                return BadRequest(getResult);

            return Ok(getResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(CarCreditScoreAddDto carCreditScoreAddDto)
        {
            var getResult = await _carCreditScoreService.AddAsync(carCreditScoreAddDto);
            if (!getResult.Success)
                return BadRequest(getResult);

            return Ok(getResult);
        }
    }
}
