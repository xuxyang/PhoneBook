using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhoneBookIdeal.Models;
using PhoneBookIdeal.Service;
using PhoneBookIdeal.Controllers;
using System.Linq;
using System.Web.Http.Results;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace PhoneBookIdeal.Tests.Controllers
{
    [TestClass]
    public class PhoneControllerTest
    {
        [TestMethod]
        public void GetAllPhones_ThreePhones()
        {
            var phones = new List<Phone>{
                new Phone { ID = 1, FirstName = "xuxia", LastName = "yang", PhoneNumber = "4193771721" },
                new Phone { ID = 2,  FirstName = "xu", LastName = "yang", PhoneNumber = "4193771722" },
                new Phone { ID = 3,  FirstName = "xia", LastName = "yang", PhoneNumber = "4193771723" }
            };
            Mock<IPhoneService> mockPhoneService = new Mock<IPhoneService>();
            mockPhoneService.Setup(service => service.GetAllPhones()).Returns(phones);

            PhoneController controller = new PhoneController(mockPhoneService.Object);
            var phoneResult = controller.GetPhones();

            Assert.AreEqual(3, phoneResult.Count());
        }

        [TestMethod]
        public void GetPhone_NotExists()
        {
            Mock<IPhoneService> mockPhoneService = new Mock<IPhoneService>();
            Phone result = null;
            mockPhoneService.Setup(service => service.GetPhone(1)).Returns(result);

            PhoneController controller = new PhoneController(mockPhoneService.Object);
            var phoneResult = controller.GetPhone(1);

            Assert.AreEqual(typeof(NotFoundResult), phoneResult.GetType());
        }

        [TestMethod]
        public void GetPhone_Exists()
        {
            Mock<IPhoneService> mockPhoneService = new Mock<IPhoneService>();
            Phone expectedResult = new Phone { ID = 1, FirstName = "xuxia", LastName = "yang", PhoneNumber = "4193771721" };
            mockPhoneService.Setup(service => service.GetPhone(1)).Returns(expectedResult);

            PhoneController controller = new PhoneController(mockPhoneService.Object);
            var actualResult = controller.GetPhone(1) as OkNegotiatedContentResult<Phone>;

            Assert.AreEqual(expectedResult.FirstName, actualResult.Content.FirstName);
            Assert.AreEqual(expectedResult.LastName, actualResult.Content.LastName);
            Assert.AreEqual(expectedResult.PhoneNumber, actualResult.Content.PhoneNumber);
        }

        [TestMethod]
        public void PutPhone_Exists()
        {
            Mock<IPhoneService> mockPhoneService = new Mock<IPhoneService>();
            Phone updatedPhone = new Phone { ID = 1, FirstName = "xuxia", LastName = "yang", PhoneNumber = "4193771721" };
            
            PhoneController controller = new PhoneController(mockPhoneService.Object);
            var result = controller.PutPhone(1, updatedPhone) as StatusCodeResult;

            mockPhoneService.Verify(service => service.UpdatePhone(updatedPhone), Times.Once());
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PutPhone_NotExists()
        {
            Mock<IPhoneService> mockPhoneService = new Mock<IPhoneService>();
            Phone updatedPhone = new Phone { ID = 1, FirstName = "xuxia", LastName = "yang", PhoneNumber = "4193771721" };
            mockPhoneService.Setup(service => service.UpdatePhone(updatedPhone)).Throws(new DbUpdateConcurrencyException());
            mockPhoneService.Setup(service => service.PhoneExists(1)).Returns(false);

            PhoneController controller = new PhoneController(mockPhoneService.Object);
            var result = controller.PutPhone(1, updatedPhone);

            mockPhoneService.Verify(service => service.UpdatePhone(updatedPhone), Times.Once());
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void PutPhone_WrongID()
        {
            Mock<IPhoneService> mockPhoneService = new Mock<IPhoneService>();
            Phone updatedPhone = new Phone { ID = 1, FirstName = "xuxia", LastName = "yang", PhoneNumber = "4193771721" };
            
            PhoneController controller = new PhoneController(mockPhoneService.Object);
            var result = controller.PutPhone(2, updatedPhone);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public void PostPhone_Success()
        {
            Mock<IPhoneService> mockPhoneService = new Mock<IPhoneService>();
            Phone newPhone = new Phone { ID = 1, FirstName = "xuxia", LastName = "yang", PhoneNumber = "4193771721" };

            PhoneController controller = new PhoneController(mockPhoneService.Object);
            var result = controller.PostPhone(newPhone) as CreatedAtRouteNegotiatedContentResult<Phone>;
            
            mockPhoneService.Verify(service => service.AddPhone(newPhone), Times.Once());
            Assert.AreEqual(newPhone.FirstName, result.Content.FirstName);
            Assert.AreEqual(newPhone.LastName, result.Content.LastName);
            Assert.AreEqual(newPhone.PhoneNumber, result.Content.PhoneNumber);
        }

        [TestMethod]
        public void DeletePhone_NotExists()
        {
            Mock<IPhoneService> mockPhoneService = new Mock<IPhoneService>();
            Phone phoneToDelete = null;
            mockPhoneService.Setup(service => service.GetPhone(1)).Returns(phoneToDelete);

            PhoneController controller = new PhoneController(mockPhoneService.Object);
            var result = controller.DeletePhone(1);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeletePhone_Exists()
        {
            Mock<IPhoneService> mockPhoneService = new Mock<IPhoneService>();
            Phone phoneToDelete = new Phone { ID = 1, FirstName = "xuxia", LastName = "yang", PhoneNumber = "4193771721" };
            mockPhoneService.Setup(service => service.GetPhone(1)).Returns(phoneToDelete);

            PhoneController controller = new PhoneController(mockPhoneService.Object);
            var result = controller.DeletePhone(1) as OkNegotiatedContentResult<Phone>;

            mockPhoneService.Verify(service => service.DeletePhone(phoneToDelete), Times.Once());
            Assert.AreEqual(phoneToDelete.FirstName, result.Content.FirstName);
            Assert.AreEqual(phoneToDelete.LastName, result.Content.LastName);
            Assert.AreEqual(phoneToDelete.PhoneNumber, result.Content.PhoneNumber);
        }
    }
}
