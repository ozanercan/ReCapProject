using Business.Abstract;
using Core.Entities.Concrete;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetFirstLastNameByEmail")]
        public async Task<IActionResult> GetFirstLastNameByEmailAsync(string email)
        {
            var result = await _userService.GetFirstNameLastNameByMailAsync(email);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(User user)
        {
            var result = await _userService.AddAsync(user);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            var result = await _userService.UpdateAsync(user);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(User user)
        {
            var result = await _userService.DeleteAsync(user);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
