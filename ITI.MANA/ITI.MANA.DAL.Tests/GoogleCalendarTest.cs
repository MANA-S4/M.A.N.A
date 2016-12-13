using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.DAL.Tests
{
    public class GoogleCalendarTest
    {
        [TestFixture]
        public class SimpleTests
        {
            [Test]
            public void GetEventsListReturnAList()
            {
                GoogleCalendar Test = new GoogleCalendar();

                Test.DefineGoogleRequestParameters();
                Test.GetListEvents();

                Assert.That(Test.Events != null);
            }
        }
    }
}
