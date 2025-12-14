using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Store.API.Services.Abstractions;
using Store.API.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController(IServiceManger _serviceManger) :ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await _serviceManger.AuthService.LoginAsync(login);
            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var result = await _serviceManger.AuthService.RegisterAsync(register);
            return Ok(result);
        }
    }
}
