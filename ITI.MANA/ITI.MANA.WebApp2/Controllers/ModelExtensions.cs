using ITI.MANA.DAL;
using ITI.MANA.WebApp.Models.CalendarViewModel;

namespace ITI.PrimarySchool.WebApp.Controllers
{
    public static class ModelExtensions
    {
        public static EventsViewModel ToEventsViewModel(this CalendarEvent @this)
        {
            return new EventsViewModel
            {
                EventId = @this.EventId,
                EventName = @this.EventName,
                EventDate = @this.Date,
                UserId = @this.UserId,
                IsPrivate = @this.Private,
                Members = @this.Members
            };
        }
    }
}
