using System;

namespace ITI.MANA.WebApp.Models.CalendarViewModel
{
    public class EventsViewModel
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }
        
        public int UserId { get; set; }

        public string Members { get; set; }

        public bool IsPrivate { get; set; }
    }
}
