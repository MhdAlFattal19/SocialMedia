using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Domain.Contracts;
using SocialMedia.Domain.Requests;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }


        #region APIS

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            _logger.LogInformation($"{nameof(AuthController)} - {nameof(Register)} - started successfully with request : {JsonConvert.SerializeObject(request)}");

            var response = await _authService.Register(request);

            _logger.LogInformation($"{nameof(AuthController)} - {nameof(Register)} - ended successfully with result : {JsonConvert.SerializeObject(response)}");
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            _logger.LogInformation($"{nameof(AuthController)} - {nameof(Login)} - started successfully with request : {JsonConvert.SerializeObject(request)}");

            var response = await _authService.Login(request);

            _logger.LogInformation($"{nameof(AuthController)} - {nameof(Login)} - ended successfully with result : {JsonConvert.SerializeObject(response)}");
            return StatusCode(response.StatusCode, response);
        }

        #endregion
    }
}
