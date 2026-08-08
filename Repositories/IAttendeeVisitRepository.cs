using ConferenceCheckInSystem.Models;
using System.Collections.Generic;

namespace ConferenceCheckInSystem.Repositories
{
    public interface IAttendeeVisitRepository
    {
        IEnumerable<AttendeeVisit> GetAll();
        AttendeeVisit? GetById(int id);
        void Add(AttendeeVisit visit);
        void Update(AttendeeVisit visit);
        void RecordCheckout(int id);
        IEnumerable<AttendeeVisit> Search(string query);
    }
}