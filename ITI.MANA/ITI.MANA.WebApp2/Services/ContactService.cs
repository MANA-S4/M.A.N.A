using ITI.MANA.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.WebApp.Services
{
    public class ContactService
    {
        readonly ContactGateway _contactGateway;
        readonly PasswordHasher _passwordHasher;

        public ContactService(ContactGateway contactGateway, PasswordHasher passwordHasher)
        {
            _contactGateway = contactGateway;
            _passwordHasher = passwordHasher;
        }

        public Result<Contact> GetById(int id)
        {
            Contact contact;
            if ((contact = _contactGateway.FindById(id)) == null) return Result.Failure<Contact>(Status.NotFound, "Contact not found.");
            return Result.Success(Status.Ok, contact);
        }

        public Result<IEnumerable<Contact>> GetAll()
        {
            return Result.Success(Status.Ok, _contactGateway.GetAll());
        }

        /// <summary>
        /// To create a contact
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public Result<Contact> CreateContact(string email, string link)
        {
            if (!IsMailValid(email)) return Result.Failure<Contact>(Status.BadRequest, "The email is not valid.");
            _contactGateway.Create(email, link);
            Contact contact = _contactGateway.FindByMail(email);
            return Result.Success(Status.Created, contact);
        }

        /// <summary>
        /// To update a contact, change informations
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public Result<Contact> UpdateContact(int contactId, string email, string link)
        {
            if (!IsMailValid(email)) return Result.Failure<Contact>(Status.BadRequest, "The mail is not valid.");
            Contact contact;
            if ((contact = _contactGateway.FindById(contactId)) == null)
            {
                return Result.Failure<Contact>(Status.NotFound, "Contact not found.");
            }

            {
                Contact c = _contactGateway.FindByMail(email);
                if (c != null && c.ContactId != contact.ContactId) return Result.Failure<Contact>(Status.BadRequest, "A contact with this name already exists.");
            }

            _contactGateway.Update(contactId, email, link);
            contact = _contactGateway.FindById(contactId);
            return Result.Success(Status.Ok, contact);
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public Result<int> DeleteContact(int contactId)
        {
            if (_contactGateway.FindById(contactId) == null) return Result.Failure<int>(Status.NotFound, "Contact not found.");
            _contactGateway.Delete(contactId);
            return Result.Success(Status.Ok, contactId);
        }

        bool IsMailValid(string email) => !string.IsNullOrWhiteSpace(email);
    }
}
