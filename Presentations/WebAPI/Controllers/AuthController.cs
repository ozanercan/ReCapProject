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
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLoginResult = _authService.Login(userForLoginDto);
            if (!userToLoginResult.Success)
                return BadRequest(userToLoginResult.Message);

            var accessTokenResult = _authService.CreateAccessToken(userToLoginResult.Data);
            if (accessTokenResult.Success)
                return Ok(accessTokenResult.Data);


            return BadRequest(accessTokenResult.Message);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExistResult = _authService.UserExist(userForRegisterDto.Email);
            if (userExistResult.Success)
                return BadRequest(userExistResult.Message);

            var registerResult = _authService.Register(userForRegisterDto);
            if (!registerResult.Success)
                return BadRequest(registerResult.Message);

            var createAccessTokenResult = _authService.CreateAccessToken(registerResult.Data);
            if (createAccessTokenResult.Success)
                return Ok(createAccessTokenResult.Data);

            return BadRequest(createAccessTokenResult.Message);
        }
    }
}
