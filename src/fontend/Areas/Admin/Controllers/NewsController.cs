using Microsoft.AspNetCore.Mvc;

namespace fontend.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult SearchandReport()
        {
            return View();
        }
    }

}
