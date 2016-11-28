using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MANA.DAL.Tests
{
    [TestFixture]
    public class SimpleTests
    {
        [Test]
        public void ShouldPass()
        {
        }

        [Test]
        public void ShouldFail()
        {
            Assert.That(3, Is.EqualTo(25));
        }
    }
}
