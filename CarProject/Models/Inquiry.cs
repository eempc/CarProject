﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CarProject.Models {
    public class Inquiry {
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string DesiredVehicleId { get; set; } // Id of the vehicle chosen by the user
        public double GetDays() {
            return (EndDate - StartDate).TotalDays + 1;
        }
    }
}
