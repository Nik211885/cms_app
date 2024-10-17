using backend.Services.CMS.News;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace backend.Controllers.CMS
{
    [Route("api/news")]
    [ApiController]
    public class CMSNewsController : ControllerBase
    {
        private readonly ICMSNewsServices _services;
        private readonly ILogger<CMSNewsController> _logger;
        public CMSNewsController(ICMSNewsServices services, ILogger<CMSNewsController> logger)
        {
            _services = services;
            _logger = logger;
        }
        [HttpGet("significant")]
        public async Task<IActionResult> GetAllNewsSignificantAsync()
        {
            _logger.LogInformation("Start running get all news significant");
            try
            {
                var result = await _services.GetNewsDescriptionSignificantAsync();
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpGet("menu")]
        public async Task<IActionResult> GetAllNewsByMenuIdAsync([Required] int menuId)
        {
            _logger.LogInformation("Start running get all news by menu id");
            try
            {
                var result = await _services.GetNewsDescriptionByMenuIdAsync(menuId);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
    }
}
