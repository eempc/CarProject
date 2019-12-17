using CarProject.Areas.Identity.Data;
using System;

namespace CarProject.Models {
    interface IBooking {
        int BookingId { get; set; }
        DateTime DateCreated { get; set; }
        string OwnerId { get; set; }
        CarProjectUser User { get; set; }
        DateTime BookingStartDateTime { get; set; }
        DateTime BookingEndDateTime { get; set; }
        decimal PricePaid { get; set; }
        bool PaymentConfirmed { get; set; }
        Guid VehicleId { get; set; }
        Vehicle Vehicle { get; set; }
    }
}
