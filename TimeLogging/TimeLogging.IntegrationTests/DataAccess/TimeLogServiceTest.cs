using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TimeLogging.Models;
using TimeLogging.DataAccess;

namespace TimeLogging.IntegrationTests
{
    [TestClass]
    public class TimeLogServiceTest
    {
        private TimeLogService tls;

        [TestInitialize]
        public void Init()
        {
            tls = new TimeLogService();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
