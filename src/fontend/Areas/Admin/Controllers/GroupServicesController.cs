using Microsoft.AspNetCore.Mvc;

namespace fontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
    }
}
