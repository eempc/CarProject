﻿using CarProject.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarProject.Models {
    public class Booking : IBooking {
        [Required, Key, Display(Name = "Booking no.")]
        public int BookingId { get; set; } // Standard int

        [Required, DataType(DataType.DateTime), Display(Name = "Order date")]
        public DateTime DateCreated { get; set; } // DateTime this entry was added to DB

        [Required, ForeignKey("User")]
        public string OwnerId { get; set; } // Which user booked the car
        public CarProjectUser User { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Booking start date")]
        public DateTime BookingStartDateTime { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Booking end date")]
        public DateTime BookingEndDateTime { get; set; }

        [Required, DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)"), Range(0, Double.PositiveInfinity), Display(Name = "Amount")]
        public decimal PricePaid { get; set; }

        public bool PaymentConfirmed { get; set; }

        [Required, ForeignKey("Vehicle")]
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
