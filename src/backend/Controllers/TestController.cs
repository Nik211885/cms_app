using backend.Helper;
using backend.Infrastructure.Data.DbContext.master;
using Dapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace backend.Controllers
{
    [Route("api/test-image")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> TestImageAsync(IFormFile? image)
        {
            var path = await FileHelper.UploadImageAsync(image);
            return Ok(path);
        }
    }
}
