using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
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
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("Proje çalıştı");
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _colorService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _colorService.GetAllAsync();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(ColorAddDto colorAddDto)
        {
            var result = await _colorService.AddAsync(colorAddDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync(ColorUpdateDto colorUpdateDto)
        {
            var result = await _colorService.UpdateAsync(colorUpdateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(Color color)
        {
            var result = await _colorService.DeleteAsync(color);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
