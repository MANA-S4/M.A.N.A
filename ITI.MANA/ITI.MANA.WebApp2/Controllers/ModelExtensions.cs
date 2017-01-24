using ITI.MANA.DAL;
using ITI.MANA.WebApp.Models.CalendarViewModel;
using ITI.MANA.WebApp.Models.ContactViewModels;
using ITI.MANA.WebApp.Models.TaskViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.WebApp.Controllers
{
    public static class ModelExtensions
    {
        public static ContactViewModel ToContactViewModel(this Contact @this)
        {
            return new ContactViewModel
            {
                ContactId = @this.ContactId,
                Email = @this.Email,
                RelationType = @this.RelationType
            };
        }

        public static TaskViewModel ToTaskViewModel(this DAL.Task @this)
        {
            return new TaskViewModel
            {
                TaskId = @this.TaskId,
                TaskName = @this.TaskName,
                TaskDate = @this.TaskDate,
                AttributeTo = @this.AttributeTo
            };
        }

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
