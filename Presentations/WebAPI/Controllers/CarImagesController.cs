using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageService _carImageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment)
        {
            _carImageService = carImageService;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromForm] CarImageAddDto carImageAddDto)
        {
            var addResult = await _carImageService.AddAsync(carImageAddDto, _webHostEnvironment);
            if (!addResult.Success)
                return BadRequest(addResult);

            return Ok(addResult);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromForm] CarImageUpdateDto carImageUpdateDto)
        {
            var updateResult = await _carImageService.UpdateAsync(carImageUpdateDto, _webHostEnvironment);
            if (!updateResult.Success)
                return BadRequest(updateResult);

            return Ok(updateResult.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm] CarImageDeleteDto carImageDeleteDto)
        {
            var deleteResult = _carImageService.Delete(carImageDeleteDto, _webHostEnvironment);
            if (!deleteResult.Success)
                return BadRequest(deleteResult);

            return Ok(deleteResult);
        }
    }
}
