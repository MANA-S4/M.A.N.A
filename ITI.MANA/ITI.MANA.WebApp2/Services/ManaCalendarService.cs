using System;
using System.Collections.Generic;
using ITI.MANA.DAL;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.MANA.WebApp.Services
{
    public class ManaCalendarService
    {
        readonly ManaCalendarGateway _manaCalendarGateway;

        public ManaCalendarService(ManaCalendarGateway manaCalendarGateway)
        {
            _manaCalendarGateway = manaCalendarGateway;
        }

        public Result<IEnumerable<CalendarEvent>> GetListEvents(int userId)
        {
            return Result.Success(Status.Ok, _manaCalendarGateway.GetListEvents(userId));
        }

        public Result<CalendarEvent> CreateEvent(string eventName, DateTime eventDate, string members, bool isPrivate, int userId)
        {
            _manaCalendarGateway.CreateEvent(eventName, eventDate, members, isPrivate, userId);
            CalendarEvent calendarEvent = _manaCalendarGateway.FindEvent(eventName, eventDate, userId);
            return Result.Success(Status.Created, calendarEvent);
        }

        internal Result<CalendarEvent> UpdateEvent(int eventId, string eventName, DateTime eventDate, string members, bool isPrivate)
        {
            if (!IsNameValid(eventName)) return Result.Failure<CalendarEvent>(Status.BadRequest, "The event name is not valid.");
            if (!IsNameValid(eventDate.ToString())) return Result.Failure<CalendarEvent>(Status.BadRequest, "The event date is not valid.");
            CalendarEvent manaEvent;
            if ((manaEvent = _manaCalendarGateway.FindById(eventId)) == null)
            {
                return Result.Failure<CalendarEvent>(Status.NotFound, "Event not found.");
            }

            _manaCalendarGateway.UpdateEvent(eventId, eventName, eventDate, members, isPrivate);
            manaEvent = _manaCalendarGateway.FindById(eventId);
            return Result.Success(Status.Ok, manaEvent);
        }

        internal Result<int> DeleteEvent(int id)
        {
            if (_manaCalendarGateway.FindById(id) == null) return Result.Failure<int>(Status.NotFound, "Event not found.");
            _manaCalendarGateway.Delete(id);
            return Result.Success(Status.Ok, id);
        }

        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);
    }
}
