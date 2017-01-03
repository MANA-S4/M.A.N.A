using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.MANA.WebApp.Server;

namespace ITI.MANA.DAL.Tests
{
    [TestFixture]
    public class MailServerTests
    {
        public UserGateway sut = new UserGateway(TestHelpers.ConnectionString);
        public MailServer mail = new MailServer();

        [Test]
        public void can_send_mail_when_user_register()
        {
            // Create a user
            string email = string.Format("user{0}@test.com", Guid.NewGuid());
            byte[] password = Guid.NewGuid().ToByteArray();

            sut.CreatePasswordUser(email, password);
            User user = sut.FindByEmail(email);

            {
                Assert.That(user.Email, Is.EqualTo(email));
                Assert.That(user.Password, Is.EqualTo(password));
            }

            {
                User u = sut.FindById(user.UserId);
                Assert.That(u.Email, Is.EqualTo(email));
                Assert.That(u.Password, Is.EqualTo(password));
            }

            // Send confirmation mail 
            mail.SendMail(email);
        }
    }
}
