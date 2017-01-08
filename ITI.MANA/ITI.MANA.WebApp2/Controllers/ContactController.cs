using ITI.MANA.DAL;
using ITI.MANA.WebApp.Authentification;
using ITI.MANA.WebApp.Models.ContactViewModels;
using ITI.MANA.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<IEnumerable<Contact>> result = _contactService.GetAll(userId);
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

        // Le build passe dans les deux conditions et renvoie une nullreference exception
        [HttpPost]
        public IActionResult CreateContact([FromBody] ContactViewModel model)
        {
            //if (model.Email == null)
            //{
            //    MailService mail = new MailService(model.Email);
            //    mail.SendInvitation(model.Email);
            //    return this.Ok();
            //}
            //else
            //{
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                Result<Contact> result = _contactService.CreateContact(model.RelationType, userId, model.Email);
                return this.CreateResult<Contact, ContactViewModel>(result, o =>
                {
                    o.ToViewModel = s => s.ToContactViewModel();
                    o.RouteName = "GetContact";
                    o.RouteValues = s => new { id = s.ContactId };
                });
            //}
        }

        [HttpPut("{contactId}")]
        public IActionResult UpdateContact(int contactId, [FromBody] ContactViewModel model)
        {
            Result<Contact> result = _contactService.UpdateContact(contactId, model.RelationType);
            return this.CreateResult<Contact, ContactViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToContactViewModel();
            });
        }

        [HttpDelete("{contactId}")]
        public IActionResult DeleteContact(int contactId)
        {
            Result<int> result = _contactService.DeleteContact(contactId);
            return this.CreateResult(result);
        }
    }
}
