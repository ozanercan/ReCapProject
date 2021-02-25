using Business.Abstract;
using Entities.Dtos;
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
        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromForm]CarImageAddDto carImageAddDto)
        {
            if (carImageAddDto.FormFile == null)
                return BadRequest("Gönderdiğiniz kayıtta resime rastlanmadı. Lütfen resim seçiniz.");

            var fileResult = await FileHelper.ImageUploadAsync(carImageAddDto.FormFile);

            if (!fileResult.Success)
            {
                return BadRequest("Resim yüklenemedi.");
            }

            carImageAddDto.ImagePath = fileResult.ShortPath;
            var addResult = _carImageService.Add(carImageAddDto);

            if (!addResult.Success)
                return BadRequest(addResult);


            return Ok(addResult.Message);
        }
    }
}
