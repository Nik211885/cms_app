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
        [HttpGet("admin/news/edit/{id}")]

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult SearchandReport()
        {
            return View();
        }
        [HttpGet("admin/news/Approve/{id}")]
        public IActionResult Approve(int id) // Nhận ID từ URL
        {

            return View();
        }
        public IActionResult WaitPublish()
        {
            return View();
        }
    }

}
