using Microsoft.AspNetCore.Mvc;

namespace fontend.Areas.Admin.Controllers
{
    public class NewsGroupController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Area("Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Area("Admin")]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
