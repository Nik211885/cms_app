using Microsoft.AspNetCore.Mvc;

namespace uc.app.cms.Areas.Admin.Controllers
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
