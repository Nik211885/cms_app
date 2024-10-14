using backend.Services.CMS.Menu;
using Microsoft.AspNetCore.Mvc;

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
    }
}
