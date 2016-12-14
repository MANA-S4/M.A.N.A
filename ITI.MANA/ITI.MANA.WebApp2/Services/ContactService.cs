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

            
        }
}
