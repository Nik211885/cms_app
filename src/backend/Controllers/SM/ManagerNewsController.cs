using backend.DTOs.CMS.Request;
using backend.Services.CMS.News;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [HttpGet("news-normal/detail")]
        public async Task<IActionResult> GetNewsNormalDetailByIdAsync([Required] int newsId)
        {
            _logger.LogInformation("Start running get news normal detail by id");
            try
            {
                var result = await _newServices.GetNewsNormalByIdAsync(userId, newsId);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpGet("news-services/detail")]
        public async Task<IActionResult> GetNewServicesDetailByIdAsync([Required] int newsId)
        {
            _logger.LogInformation("Start running get news services detail by id");
            try
            {
                var result = await _newServices.GetNewsServicesByIdAsync(userId, newsId);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("create-news-normal")]
        public async Task<IActionResult> CreateNewsNormalAsync([FromBody] CreateNewsNormalRequest request)
        {
            _logger.LogInformation("Start build function create news normal");
            try
            {
                var result = await _newServices.CreateNewsNormalAsync(userId, request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("create-news-services")]
        public async Task<IActionResult> CreateNewsNormalAsync([FromBody] CreateNewsServicesRequest request)
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
        [HttpPut("update-news-normal")]
        public async Task<IActionResult> UpdateNewsNormalAsync([Required] int newsId, [FromBody] UpdateNewsNormalRequest request)
        {
            _logger.LogInformation("Start build function update news normal");
            try
            {
                var result = await _newServices.UpdateNewsNormalAsync(userId, newsId, request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPut("update-news-services")]
        public async Task<IActionResult> UpdateNewsServicesAsync([Required] int newsId, [FromBody] UpdateNewsServicesRequest request)
        {
            _logger.LogInformation("Start build function update news services");
            try
            {
                var result = await _newServices.UpdateNewsServicesAsync(userId, newsId, request);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteNewsAsync([Required] int newsId)
        {
            _logger.LogInformation("Start running function delete news");
            try
            {
                await _newServices.DeleteNewsAsync(userId, newsId);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
    }
}
