using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using JoinCar.Controllers;
using JoinCar.Database.Entities;
using JoinCar.Database.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace JoinCar.Tests.Controllers
{
    /// <summary>
    /// Summary description for TripsControllerTests
    /// </summary>
    [TestClass]
    public class TripsControllerTests
    {
        [TestMethod]
        public void Index()
        {
            var mockRepo = new Mock<ITripsRepository>();
            TripsController controller = new TripsController(mockRepo.Object, null, null);
            ViewResult result = controller.Index(string.Empty, string.Empty, null, null) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ArchivedTripsList()
        {
            var mockRepo = new Mock<ITripsRepository>();
            TripsController controller = new TripsController(mockRepo.Object, null, null);
            ViewResult result = controller.ArchivedTripsList(string.Empty, string.Empty, null, null) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details_notFound()
        {
            var mockTripsRepo = new Mock<ITripsRepository>();
            var mockIntrestsRepo = new Mock<IInterestsRepository>();
            TripsController controller = new TripsController(mockTripsRepo.Object, null, mockIntrestsRepo.Object);
            var result = controller.Details(9) as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Details()
        {
            var mockTripsRepo = new Mock<ITripsRepository>();
            var mockIntrestsRepo = new Mock<IInterestsRepository>();
            var mockTrip = new Mock<Trip>().Object;
            mockTrip.Id = 1;
            mockTripsRepo.Object.AddTrip(mockTrip);
            TripsController controller = new TripsController(mockTripsRepo.Object, null, mockIntrestsRepo.Object);
            var result = controller.Details(1) as ViewResult;
            Assert.IsNull(result);
        }


        [TestMethod]
        public void Details_nullId()
        {
            var mockTripsRepo = new Mock<ITripsRepository>();
            var mockIntrestsRepo = new Mock<IInterestsRepository>();
            TripsController controller = new TripsController(mockTripsRepo.Object, null, mockIntrestsRepo.Object);
            var result = controller.Details(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void Create_validModel()
        {

        }

        [TestMethod]
        public void Create_invalidModel()
        {

        }

        [TestMethod]
        public void Edit()
        {

        }

        [TestMethod]
        public void Edit_nullId()
        {

        }

        [TestMethod]
        public void Edit_validModel()
        {

        }

        [TestMethod]
        public void Edit_invalidModel()
        {

        }

        [TestMethod]
        public void Delete()
        {

        }

        [TestMethod]
        public void Delete_nullId()
        {

        }

        [TestMethod]
        public void DeleteConfirmed()
        {

        }

        [TestMethod]
        public void DeleteConfirmed_nullId()
        {

        }

        [TestMethod]
        public void MyInterests()
        {

        }

        [TestMethod]
        public void JoinCar()
        {

        }

        [TestMethod]
        public void JoinCar_nullId()
        {

        }

        [TestMethod]
        public void LeaveCar()
        {

        }

        [TestMethod]
        public void LeaveCar_nullId()
        {

        }

        [TestMethod]
        public void CreateOpinion()
        {

        }

        [TestMethod]
        public void CreateOpinion_nullId()
        {

        }

        [TestMethod]
        public void CreateOpinion_validModel()
        {

        }

        [TestMethod]
        public void CreateOpinion_invalidModel()
        {

        }
    }
}
