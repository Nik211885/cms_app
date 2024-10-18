using backend.DTOs.CMS.Request;
using backend.Services.CMS.Menu;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace backend.Controllers.CMS
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuServices _menuServices;
        private readonly ILogger<MenuController> _logger;
        public MenuController(IMenuServices menuServices, ILogger<MenuController> logger)
        {
            _menuServices = menuServices;
            _logger = logger;
        }
        [HttpGet("type")]
        public async Task<IActionResult> GetMenuReponseAsync(string? type, bool getChild)
        {
            _logger.LogInformation("Start run get menu services");
            try
            {
                var result = await _menuServices.GetMenuAsync(type, getChild);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors: {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateMenuAsync([FromBody] CreateMenuRequest request)
        {
            var result = await _menuServices.CreateMenuAsync(request);
            return ResponseMessage.Success(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMenuAsync(int id)
        {
            _logger.LogInformation("Start running delete menu");
            try
            {
                await _menuServices.DeleteMenuAsync(id);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors : {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMenuAsync([Required]int menuId, [FromBody] UpdateMenuRequest request)
        {
            _logger.LogInformation("Start update menu");
            try
            {
                var result = await _menuServices.UpdateMenuAsync(menuId,request);
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
