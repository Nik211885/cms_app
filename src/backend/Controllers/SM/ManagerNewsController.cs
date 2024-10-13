using backend.DTOs.CMS.Request;
using backend.Services.CMS.News;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.SM
{
    [Route("api/manager-news")]
    [ApiController]
    public class ManagerNewsController : ControllerBase
    {
        private readonly INewsServices _newServices;
        private readonly ILogger<ManagerNewsController> _logger;
        private readonly int userId = 2;
        public ManagerNewsController(INewsServices newServices, ILogger<ManagerNewsController> logger)
        {
            _newServices = newServices;
            _logger = logger;
        }
        [HttpPost("create-news-normal")]
        public async Task<IActionResult> CreateNewNewsNormalAsync([FromBody] CreateNewsNormalRequest request)
        {
            _logger.LogInformation("Start build function create news normal");
            try
            {
                var result = await _newServices.CreateNewsNormalAsync(userId, request);
                return ResponseMessage.Success(result);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("create-news-services")]
        public async Task<IActionResult> CreateNewNewsNormalAsync([FromBody] CreateNewsServicesRequest request)
        {
            _logger.LogInformation("Start build function create news services");
            try
            {
                var result = await _newServices.CreateNewsServicesAsync(userId, request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
    }
}
