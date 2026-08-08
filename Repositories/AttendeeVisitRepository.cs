using ConferenceCheckInSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceCheckInSystem.Repositories
{
    public class AttendeeVisitRepository : IAttendeeVisitRepository
    {
        private static readonly List<AttendeeVisit> _attendees = new()
        {
            new AttendeeVisit
            {
                Id = 1,
                TicketNumber = "TCK-1001",
                FirstName = "Jane",
                LastName = "Doe",
                Organization = "TechCorp",
                ContactNumber = "09123456789",
                Email = "jane.doe@techcorp.com",
                EventName = "Annual Tech Summit 2026",
                CheckInTime = DateTime.Now.AddHours(-2),
                Status = "Present",
                Notes = "VIP Attendee"
            }
        };

        public IEnumerable<AttendeeVisit> GetAll() => _attendees;

        public AttendeeVisit? GetById(int id) => _attendees.FirstOrDefault(a => a.Id == id);

        public void Add(AttendeeVisit visit)
        {
            visit.Id = _attendees.Count > 0 ? _attendees.Max(a => a.Id) + 1 : 1;
            _attendees.Add(visit);
        }

        public void Update(AttendeeVisit visit)
        {
            var existing = GetById(visit.Id);
            if (existing != null)
            {
                existing.TicketNumber = visit.TicketNumber;
                existing.FirstName = visit.FirstName;
                existing.LastName = visit.LastName;
                existing.Organization = visit.Organization;
                existing.ContactNumber = visit.ContactNumber;
                existing.Email = visit.Email;
                existing.EventName = visit.EventName;
                existing.Notes = visit.Notes;
            }
        }

        public void RecordCheckout(int id)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                existing.CheckOutTime = DateTime.Now;
                existing.Status = "Left Event";
            }
        }

        public IEnumerable<AttendeeVisit> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return GetAll();

            query = query.Trim().ToLower();
            return _attendees.Where(a =>
                a.TicketNumber.ToLower().Contains(query) ||
                a.FirstName.ToLower().Contains(query) ||
                a.LastName.ToLower().Contains(query) ||
                a.Organization.ToLower().Contains(query) ||
                a.EventName.ToLower().Contains(query));
        }
    }
}