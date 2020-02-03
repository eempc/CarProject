using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarProject.Models;

namespace UnitTestProject1 {
    [TestClass]
    class BookingModelTest {
        // Things to test for in the Booking model:
        // Check the format of DateTime.Now
        // Ensure the number of days between booking dates is integer
        // Check PricePaid decimal is decimal
        // Divide PricePaid by number of days

        Booking testBooking = new Booking {
            BookingId = 1,
            DateCreated = DateTime.Now
        };

    }
}
