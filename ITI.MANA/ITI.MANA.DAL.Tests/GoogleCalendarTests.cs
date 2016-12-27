using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Google.Apis.Auth.OAuth2.Responses;


namespace ITI.MANA.DAL.Tests
{
    [TestFixture]
    public class GoogleCalendarTests
    {
        public GoogleCalendarGateway sut = new GoogleCalendarGateway(TestHelpers.ConnectionString);

        [Test]
        public void GetTokenResponseReturnValue()
        {
            TokenResponse token = sut.GetResponseToken(13);
            Assert.That(token != null);
        }
    }
}
