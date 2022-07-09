using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monarchs.Common.Constants;
using Monarchs.Common.Interfaces;
using Monarchs.Common.ViewModels;
using System.Net.Mime;

namespace Monarchs.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        /// <summary>
        /// Returns a token if username and password match
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = _loginService.Authenticate(userLogin);

            if (user != null)
            {
                var token = _loginService.GenerateToken(user);
                return Ok(token);
            }

            _logger.LogInformation(LogMessages.UserNotFound);
            return NotFound(LogMessages.UserNotFound);
        }

    }
}
