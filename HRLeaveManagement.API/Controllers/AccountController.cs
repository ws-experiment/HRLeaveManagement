using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authenticationService;

        public AccountController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authenticationService.Register(request));
        }
    }
}
