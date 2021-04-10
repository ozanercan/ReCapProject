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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToLoginResult = await _authService.LoginAsync(userForLoginDto);
            if (!userToLoginResult.Success)
                return BadRequest(userToLoginResult);

            var accessTokenResult = await _authService.CreateAccessTokenAsync(userToLoginResult.Data);
            if (!accessTokenResult.Success)
                return BadRequest(accessTokenResult);

            accessTokenResult.Data.User = userToLoginResult.Data;
            return Ok(accessTokenResult);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(UserForRegisterDto userForRegisterDto)
        {
            var registerResult = await _authService.RegisterAsync(userForRegisterDto);
            if (!registerResult.Success)
                return BadRequest(registerResult);

            var createAccessTokenResult = await _authService.CreateAccessTokenAsync(registerResult.Data);
            if (createAccessTokenResult.Success)
                return Ok(createAccessTokenResult);

            return BadRequest(createAccessTokenResult);
        }
    }
}
