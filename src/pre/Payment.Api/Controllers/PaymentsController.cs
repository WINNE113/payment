using Microsoft.AspNetCore.Mvc;

namespace Payment.Api.Controllers
{
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
