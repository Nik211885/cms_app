using Microsoft.AspNetCore.Mvc;

namespace fontend.Areas.Admin.Controllers
{
    [Area("Admin")] // Đánh dấu rằng controller thuộc area Admin
    public class ServicesNewsController : Controller
    {
        // Phương thức Index
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SearchAndReport()
        {
            return View();
        }
        // Phương thức Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpGet("admin/servicesnews/edit/{id}")]
        public IActionResult Edit(int id) // Nhận ID từ URL
        {
         
            return View();
        }
        [HttpGet("admin/servicesnews/Approve/{id}")]
        public IActionResult Approve(int id) // Nhận ID từ URL
        {

            return View();
        }

    }
}
