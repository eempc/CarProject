using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarProject.Models {
    public class Vehicle {
        [Required, Key]
        public Guid VehicleId { get; set; }

        [Required]
        public string RegistrationMark { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public Size Size { get; set; }

        [Required, Range(0, 9)]
        public int Seats { get; set; }

        [Required, DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }

        [NotMapped]
        public string ImageFile { 
            get {
                return Make.ToLower() + Model.ToLower() + ".png";
            }
        }
    }

    public enum VehicleType {
        Other = 0,
        Car = 1,
        Van = 2,
        Motorcycle = 3
    }

    public enum Size {
        Other = 0,
        Small,
        Medium,
        Large
    }
}
