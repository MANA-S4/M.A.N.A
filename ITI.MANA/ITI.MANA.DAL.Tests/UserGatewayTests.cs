﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.DAL.Tests
{
    [TestFixture]
    public class UserGatewayTests
    {
        public UserGateway sut = new UserGateway(TestHelpers.ConnectionString);

        [Test]
        public void can_create_find_update_and_delete_user()
        {        
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

            {
                email = string.Format("user{0}@test.com", Guid.NewGuid());
                sut.UpdateEmail(user.UserId, email);
            }

            {
                User u = sut.FindById(user.UserId);
                Assert.That(u.Email, Is.EqualTo(email));
                Assert.That(u.Password, Is.EqualTo(password));
            }

            {
                sut.Delete(user.UserId);
                Assert.That(sut.FindById(user.UserId), Is.Null);
            }
        }

        /*[Test]
        public void can_create_google_user()
        {
            string email = string.Format("user{0}@test.com", Guid.NewGuid());
            string accessToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

            sut.CreateGoogleUser(email, accessToken);
            User user = sut.FindByEmail(email);

            Assert.That(user.GoogleRefreshToken, Is.EqualTo(accessToken));

            accessToken = Guid.NewGuid().ToString().Replace("-", string.Empty);
            sut.UpdateGoogleToken(user.UserId, accessToken);

            user = sut.FindById(user.UserId);
            Assert.That(user.GoogleRefreshToken, Is.EqualTo(accessToken));

            sut.Delete(user.UserId);
        }*/

        [Test]
        public void can_create_microsoft_user()
        {        
            string email = string.Format("user{0}@test.com", Guid.NewGuid());
            string accessToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

            sut.CreateMicrosoftUser(email, accessToken);
            User user = sut.FindByEmail(email);

            Assert.That(user.MicrosoftAccessToken, Is.EqualTo(accessToken));

            accessToken = Guid.NewGuid().ToString().Replace("-", string.Empty);
            sut.UpdateMicrosoftToken(user.UserId, accessToken);

            user = sut.FindById(user.UserId);
            Assert.That(user.MicrosoftAccessToken, Is.EqualTo(accessToken));

            sut.Delete(user.UserId);
        }
    }
}
