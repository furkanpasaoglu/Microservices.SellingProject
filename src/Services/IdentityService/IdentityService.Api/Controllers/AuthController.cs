using IdentityServer.Application.Models;
using IdentityServer.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(LoginResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequestModel)
        {
            if(!ModelState.IsValid)
                throw new UnauthorizedAccessException();

            var result = await _identityService.Login(loginRequestModel);

            return Ok(result);
        }
    }
}
