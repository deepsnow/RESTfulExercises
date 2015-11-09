using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using Moq;
using TimeLogging.Models;
using TimeLogging.DataAccess;
using System.Linq.Expressions;

namespace TimeLogging.Tests.DataAccess
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


    }
}
