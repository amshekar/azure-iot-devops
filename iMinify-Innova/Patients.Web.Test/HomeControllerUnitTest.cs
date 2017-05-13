using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patients.Web.Controllers;
using System.Web.Mvc;

namespace Patients.Web.Test
{
    [TestClass]
    public class HomeControllerUnitTest
    {
        [TestMethod]
        public void Test_ViewIndex()
        {
            HomeController homeController = new HomeController();
            ActionResult result = homeController.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Test_Index()
        {
            HomeController homeController = new HomeController();
            ActionResult result = homeController.Index() as ActionResult;
            Assert.IsNotNull(result);
        }
    }
}
