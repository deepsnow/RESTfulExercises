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
        private Container container;
        private Mock<IIimeLogService> tlsMock;

        [TestInitialize]
        public void Init()
        {
            //container = new Container();
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
            tlsMock.Verify(t => t.GetFiveLatestEntries(), Times.Once);
        }

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
