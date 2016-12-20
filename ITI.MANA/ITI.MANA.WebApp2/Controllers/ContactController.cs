using ITI.MANA.DAL;
using ITI.MANA.WebApp.Authentification;
using ITI.MANA.WebApp.Models.ContactViewModels;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ContactController : Controller
    {
        readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult GetContactList()
        {
            Result<IEnumerable<Contact>> result = _contactService.GetAll();
            return this.CreateResult<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToContactViewModel());
            });
        }

        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult GetContactById(int id)
        {
            Result<Contact> result = _contactService.GetById(id);
            return this.CreateResult<Contact, ContactViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToContactViewModel();
            });
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] ContactViewModel model)
        {
            Result<Contact> result = _contactService.CreateContact(model.Email, model.Link);
            return this.CreateResult<Contact, ContactViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToContactViewModel();
                o.RouteName = "GetContact";
                o.RouteValues = s => new { id = s.ContactId };
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] ContactViewModel model)
        {
            Result<Contact> result = _contactService.UpdateContact(id, model.Email, model.Link);
            return this.CreateResult<Contact, ContactViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToContactViewModel();
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            Result<int> result = _contactService.DeleteContact(id);
            return this.CreateResult(result);
        }
    }
}
