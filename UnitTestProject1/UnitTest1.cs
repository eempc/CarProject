using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarProject.Models;
using System;

namespace UnitTestProject1 {
    [TestClass]
    public class UnitTest1 {
        Vehicle testVehicle = new Vehicle {
            VehicleId = new Guid(),
            RegistrationMark = "AA11AAA",
            VehicleType = VehicleType.Car,
            Make = "Audi",
            Model = "A3",
            Size = Size.Medium,
            Seats = 5,
            Rate = 12.50M
        };

        // Unit test of the vehicle model, test the get image file name
        [TestMethod]
        public void VehicleMakeModelToGetImageFile() {
            string expectedResult = "audia3.png";
            string actualResult = testVehicle.ImageFile;

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
