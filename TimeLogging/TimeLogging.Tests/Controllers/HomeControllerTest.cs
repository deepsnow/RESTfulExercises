using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeLogging;
using TimeLogging.Controllers;
using SimpleInjector;
using Moq;
using TimeLogging.DataAccess;
using TimeLogging.Models;

namespace TimeLogging.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IIimeLogService> tlsMock;

        [TestInitialize]
        public void Init()
        {
            tlsMock = new Mock<IIimeLogService>();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(tlsMock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            tlsMock.Verify(t => t.GetEntriesByDate(), Times.Once);
        }

        [TestMethod]
        public void CreateGet()
        {
            // Arrange
            HomeController controller = new HomeController(tlsMock.Object);

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePost()
        {
            // Arrange
            HomeController controller = new HomeController(tlsMock.Object);
            var log = new Log()
            {
                UserId = "doug",
                Billable = false,
                StartTime = DateTime.Now.AddHours(-1),
                EndTime = DateTime.Now,
                Comment = "test entry",
            };

            // Act
            ViewResult result = controller.Create(log) as ViewResult;

            // Assert
            tlsMock.Verify(t => t.AddLog(It.Is<Log>(l => l.Equals(log))), Times.Once);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            HomeController controller = new HomeController(tlsMock.Object);

            // Act
            ViewResult result = controller.Details(5) as ViewResult;

            // Assert
            tlsMock.Verify(t => t.FindLog(5), Times.Once);
        }

        //[TestMethod]
        //public void Index()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController(tlsMock.Object);

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    tlsMock.Verify(t => t.GetFiveLatestEntries(), Times.Once);
        //}

        //[TestMethod]
        //public void About()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.About() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        //}

        //[TestMethod]
        //public void Contact()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Contact() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}
    }
}
