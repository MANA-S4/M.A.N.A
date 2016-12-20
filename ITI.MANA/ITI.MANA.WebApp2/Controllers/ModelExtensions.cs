using ITI.MANA.DAL;
using ITI.MANA.WebApp.Models.ContactViewModels;
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
                Link = @this.Link
            };
        }

    }
}
