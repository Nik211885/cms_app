using backend.Services.CMS.MenuType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace backend.Controllers.SM
{
    [Route("api/manager-menu-types")]
    [ApiController]
    public class ManagerMenuTypeController : ControllerBase
    {
        private readonly IMenuTypeServices _menuTypeServices;
        private readonly ILogger<ManagerMenuTypeController> _logger;
        public ManagerMenuTypeController(IMenuTypeServices menuTypeServices, ILogger<ManagerMenuTypeController> logger)
        {
            _menuTypeServices = menuTypeServices;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenuTypeAsync()
        {
            _logger.LogInformation("Start running get all menu type");
            try
            {
                var result = await _menuTypeServices.GetAllMenuTypeAsync();
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMenuTypeAsync([Required] int id, [Required] string name)
        {
            _logger.LogInformation("Start running update menu type");
            try
            {
                var result = await _menuTypeServices.UpdateMenuTypeAsync(id,name);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateMenuTypeAsync([Required] string nameType)
        {
            _logger.LogInformation("Start running create menu type");
            try
            {
                var result = await _menuTypeServices.CreateMenuTypeAsync(nameType);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMenuTypeAsync([Required] int id)
        {
            _logger.LogInformation("Start running delete menu type");
            try
            {
                await _menuTypeServices.DeleteMenuTypeAsync(id);
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
