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
        
        public ContactService(ContactGateway contactGateway)
        {
            _contactGateway = contactGateway;
        }

        public Result<Contact> GetById(int id)
        {
            Contact contact;
            if ((contact = _contactGateway.FindById(id)) == null) return Result.Failure<Contact>(Status.NotFound, "Contact not found.");
            return Result.Success(Status.Ok, contact);
        }

        public int GetIdByMail(string email)
        {
            int contactId;
            if ((contactId = _contactGateway.FindIdByMail(email)) == 0) return 0;
            return contactId;
        }

        public Result<IEnumerable<Contact>> GetAll(int userId)
        {
            return Result.Success(Status.Ok, _contactGateway.GetAll(userId));
        }

        /// <summary>
        /// To create a contact
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public Result<Contact> CreateContact(string relationType, int userId, string email)
        {
            _contactGateway.Create(userId, relationType, GetIdByMail(email));
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
        public Result<Contact> UpdateContact(int contactId, string relationType)
        {
            //if (!IsMailValid(email)) return Result.Failure<Contact>(Status.BadRequest, "The mail is not valid.");
            Contact contact;
            if ((contact = _contactGateway.FindById(contactId)) == null)
            {
                return Result.Failure<Contact>(Status.NotFound, "Contact not found.");
            }

            {
                Contact c = _contactGateway.FindById(contactId);
                if (c != null && c.ContactId != contact.ContactId) return Result.Failure<Contact>(Status.BadRequest, "A contact with this name already exists.");
            }

            _contactGateway.Update(contactId, relationType);
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
            if (_contactGateway.FindById(contactId) == null) return Result.Failure<int>(Status.NotFound, "Contact not found");
            _contactGateway.Delete(contactId);
            return Result.Success(Status.Ok, contactId);
        }

        bool IsMailValid(string email) => !string.IsNullOrWhiteSpace(email);
    }
}
