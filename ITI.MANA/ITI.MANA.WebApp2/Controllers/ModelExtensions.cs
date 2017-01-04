using ITI.MANA.DAL;
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
                Name = @this.Name,
                Date = @this.Date,
                IsFinish = @this.IsFinish
            };
        }

    }
}
