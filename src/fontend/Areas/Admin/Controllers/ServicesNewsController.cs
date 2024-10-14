using Microsoft.AspNetCore.Mvc;

namespace uc.app.cms.Areas.Admin.Controllers
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

        // Phương thức Edit với id được truyền từ URL
        [HttpGet("admin/servicesnews/edit/{id}")]
        public IActionResult Edit(int id) // Nhận ID từ URL
        {
            // Nếu bạn cần xử lý gì với ID ở đây, bạn có thể làm
            return View();
        }
    }
}
