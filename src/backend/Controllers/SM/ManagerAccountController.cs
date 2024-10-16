using backend.DTOs.SM.Request;
using backend.Services.SM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.SM
{
    [Route("api/manager-account")]
    [ApiController]
    public class ManagerAccountController : ControllerBase
    {
        private readonly ILogger<ManagerAccountController> _logger;
        private readonly IAccountServices _accountServices;
        private readonly int userId = 2;
        // claim contain user name
        public ManagerAccountController(ILogger<ManagerAccountController> logger, IAccountServices accountServices)
        {
            _logger = logger;
            _accountServices = accountServices;
        }
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateNewAccountAsync([FromBody] CreateAccountRequest request)
        {
            _logger.LogInformation("Start running function login");
            try
            {
                var result = await _accountServices.CreateNewAccountAsync(request);
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
