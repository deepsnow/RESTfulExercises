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
        //private Mock<ITimeLoggingContext> contextMock;
        //private Mock<DbSet<Log>> logsMock;
        //private Mock<IQueryableWrapper> linqMock;
        private TimeLogService tls;

        [TestInitialize]
        public void Init()
        {
            //contextMock = new Mock<ITimeLoggingContext>();
            //logsMock = new Mock<DbSet<Log>>();
            //contextMock.Setup(c => c.Logs).Returns(logsMock.Object);
            //linqMock = new Mock<IQueryableWrapper>();
            //tls = new TimeLogService(contextMock.Object, linqMock.Object);

            tls = new TimeLogService();
        }

        //[TestMethod]
        //public void GetEntriesByDate_WhereCalledWithThreeConstraints()
        //{
        //    // Arrange
         
        //    // Act
        //    var result = tls.GetEntriesByDate();

        //    // Assert
        //    linqMock.Verify(l => l.Where(logsMock.Object, r =>
        //                    (r.StartTime.Day == DateTime.UtcNow.Day)
        //                    && (r.StartTime.Month == DateTime.UtcNow.Month)
        //                    && (r.StartTime.Year == DateTime.UtcNow.Year)));

        //    // Why does the following fail with "Expected invocation on the mock at least once, but was never performed: l => l.Where<Log>(.logsMock.Object, .exp)"???

        //    //Expression<Func<Log, bool>> exp = r =>
        //    //                (r.StartTime.Day == DateTime.UtcNow.Day)
        //    //                && (r.StartTime.Month == DateTime.UtcNow.Month)
        //    //                && (r.StartTime.Year == DateTime.UtcNow.Year);
        //    //linqMock.Verify(l => l.Where(logsMock.Object, exp));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(Moq.MockException))]
        //public void GetEntriesByDate_WhereCalledWithIncorrectConstraints_FailureDetected()
        //{
        //    // Arrange

        //    // Act
        //    var result = tls.GetEntriesByDate();

        //    // Assert
        //    linqMock.Verify(l => l.Where(logsMock.Object, r =>
        //                    (r.StartTime.Day == DateTime.UtcNow.Day)
        //                    && (r.StartTime.Month == DateTime.UtcNow.Month)
        //                    && (r.StartTime.Hour == DateTime.UtcNow.Hour))); // production code compares Year not Hour
        //}
    }
}
