using Microsoft.AspNetCore.Mvc;

namespace ConferenceCheckInSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Attendee");
        }
    }
}