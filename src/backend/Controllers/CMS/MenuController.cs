using backend.DTOs.CMS.Request;
using backend.Services.CMS.Menu;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("create-menu-parent")]
        public async Task<IActionResult> CreateMenuServicesAsync([FromBody] string name)
        {
            // fake
            var menuTypeId = 5;
            _logger.LogInformation("Start running create menu parent");
            try
            {
                // :))
                var result = await _menuServices.CreateMenuParentAsync(new CreateMenuParentRequest(name, menuTypeId));
                return ResponseMessage.Success(result);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Errors : {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
        [HttpPost("create-menu-child")]
        public async Task<IActionResult> CreateMenuChildServicesAsync([FromBody] CreateMenuChildRequest request)
        {
            _logger.LogInformation("Start running create menu child");
            try
            {
                var result = await _menuServices.CreateMenuChildAsync(new CreateMenuChildRequest(request.name, request.parent_menu_id));
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Errors : {ex.Message}");
                return ResponseMessage.Warning(ex.Message);
            }
        }
    }
}
