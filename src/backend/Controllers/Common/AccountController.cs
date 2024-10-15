using backend.DTOs.Common.Request;
using backend.Services.SM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Common
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        private readonly ILogger<AccountController> _logger;
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

    }
}
