using ConferenceCheckInSystem.Models;
using ConferenceCheckInSystem.Repositories;
using ConferenceCheckInSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceCheckInSystem.Controllers
{
    [Authorize]
    public class AttendeeController : Controller
    {
        private readonly IAttendeeVisitRepository _attendeeRepository;

        public AttendeeController(IAttendeeVisitRepository attendeeRepository)
        {
            _attendeeRepository = attendeeRepository;
        }

        // View Monitoring List + Search
        public IActionResult Index(string? searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;
            var list = _attendeeRepository.Search(searchQuery ?? string.Empty);
            return View(list);
        }

        // Register Attendee (Check-In) - GET
        [HttpGet]
        public IActionResult Create() => View();

        // Register Attendee (Check-In) - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AttendeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var visit = new AttendeeVisit
            {
                TicketNumber = model.TicketNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Organization = model.Organization,
                ContactNumber = model.ContactNumber,
                Email = model.Email,
                EventName = model.EventName,
                Notes = model.Notes,
                CheckInTime = DateTime.Now,
                Status = "Present"
            };

            _attendeeRepository.Add(visit);
            TempData["Success"] = "Attendee registered and checked in successfully.";
            return RedirectToAction("Index");
        }

        // Edit Attendee - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _attendeeRepository.GetById(id);
            if (item == null) return NotFound();

            var vm = new AttendeeViewModel
            {
                Id = item.Id,
                TicketNumber = item.TicketNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Organization = item.Organization,
                ContactNumber = item.ContactNumber,
                Email = item.Email,
                EventName = item.EventName,
                Notes = item.Notes,
                CheckInTime = item.CheckInTime,
                CheckOutTime = item.CheckOutTime,
                Status = item.Status
            };

            return View(vm);
        }

        // Edit Attendee - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AttendeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var visit = new AttendeeVisit
            {
                Id = model.Id,
                TicketNumber = model.TicketNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Organization = model.Organization,
                ContactNumber = model.ContactNumber,
                Email = model.Email,
                EventName = model.EventName,
                Notes = model.Notes
            };

            _attendeeRepository.Update(visit);
            TempData["Success"] = "Attendee updated successfully.";
            return RedirectToAction("Index");
        }

        // View Details - GET
        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = _attendeeRepository.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Record Check-Out - GET (Confirmation)
        [HttpGet]
        public IActionResult Checkout(int id)
        {
            var item = _attendeeRepository.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Record Check-Out - POST
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public IActionResult CheckoutConfirmed(int id)
        {
            _attendeeRepository.RecordCheckout(id);
            TempData["Success"] = "Attendee checked out successfully.";
            return RedirectToAction("Index");
        }
    }
}