using backend.DTOs.Common.Request;
using backend.DTOs.SM.Request;
using backend.Services.SM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Common
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        private readonly ILogger<AccountController> _logger;
        private readonly int userId = 2;
        private readonly string userName = "nik";
        public AccountController(IAccountServices accountServices, ILogger<AccountController> logger)
        {
            _logger = logger;
            _accountServices = accountServices;
        }
        //[HttpPost("login")]
        //public async Task<IActionResult> LoginAsync()
        //{

        //}
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AccountLoginRequest request)
        {
            _logger.LogInformation("Start running function login");
            try
            {
                var result = await _accountServices.LoginAsync(request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors :{ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _logger.LogInformation("Running logout function profile account");
            _accountServices.Logout(userName);
            return ResponseMessage.Success();
        }
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            _logger.LogInformation("Running function change password");
            try
            {
                var jwt = await _accountServices.ChangePasswordAsync(userId, request);
                return ResponseMessage.Success(jwt);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors :{ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] JwtAuthResult token)
        {
            _logger.LogInformation("Running function refresh token");
            try
            {
                var jwt = _accountServices.GetTokenWhenAccessTokenHasExprise(token);
                return ResponseMessage.Success(jwt);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}", ex);
                return ResponseMessage.Warning(ex.Message);
            }
        }

    }
}
