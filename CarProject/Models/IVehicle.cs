using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarProject.Models {
    interface IVehicle {
        Guid VehicleId { get; set; }
        string RegistrationMark { get; set; }
        VehicleType VehicleType { get; set; }
        string Make { get; set; }
        string Model { get; set; }
        Size Size { get; set; }
        int Seats { get; set; }
        decimal Rate { get; set; }
        string ImageFile { get; }
        }
}
