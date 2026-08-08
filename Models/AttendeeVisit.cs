using System;

namespace ConferenceCheckInSystem.Models
{
    public class AttendeeVisit
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty; // Company or School
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Status { get; set; } = "Present"; // "Present" or "Left Event"
        public string? Notes { get; set; }
    }
}