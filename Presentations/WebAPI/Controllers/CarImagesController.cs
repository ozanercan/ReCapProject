﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("getlistbycarid")]
        public async Task<IActionResult> GetListByCarIdAsync(int carId)
        {
            var carImagesResult = await _carImageService.GetAllByCarIdAsync(carId);
            if (!carImagesResult.Success)
                return BadRequest(carImagesResult);

            return Ok(carImagesResult);
        }

        [HttpPost("addPrimitive")]
        public async Task<IActionResult> AddAsync(int carId, List<IFormFile> formFiles)
        {
            if (formFiles.Count == 0)
                return BadRequest(Messages.CarImageCountInvalid);

            IResult lastAddResult = new ErrorResult();
            foreach (var formFile in formFiles)
            {
                var addResult = await _carImageService.AddAsync(new CarImageAddDto() { CarId = carId, FormFile = formFile });

                lastAddResult = addResult;
                if (!addResult.Success)
                    return BadRequest(addResult);
            }
            return Ok(lastAddResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromForm] CarImageAddDto carImageAddDto)
        {
            var addResult = await _carImageService.AddAsync(carImageAddDto);
            if (!addResult.Success)
                return BadRequest(addResult);

            return Ok(addResult);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromForm] CarImageUpdateDto carImageUpdateDto)
        {
            var updateResult = await _carImageService.UpdateAsync(carImageUpdateDto);
            if (!updateResult.Success)
                return BadRequest(updateResult);

            return Ok(updateResult.Message);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(CarImageDeleteDto carImageDeleteDto)
        {
            var deleteResult = await _carImageService.DeleteAsync(carImageDeleteDto);
            if (!deleteResult.Success)
                return BadRequest(deleteResult);

            return Ok(deleteResult);
        }


        [HttpDelete("deletebyid")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var deleteResult = await _carImageService.DeleteByIdAsync(id);
            if (!deleteResult.Success)
                return BadRequest(deleteResult);

            return Ok(deleteResult);
        }
    }
}
