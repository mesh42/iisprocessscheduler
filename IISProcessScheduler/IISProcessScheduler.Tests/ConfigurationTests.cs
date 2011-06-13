using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace IISProcessScheduler.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void ReadTouchUrlTest()
        {
            var section = ConfigurationManager.GetSection("IISProcessBehavior");
        }
    }
}
