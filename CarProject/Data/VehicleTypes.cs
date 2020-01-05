using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarProject.Models;

namespace CarProject.Data {
    public class VehicleTypes {
        public static List<VehicleInfo> GetVehicleTypes() {
            List<VehicleInfo> list = new List<VehicleInfo> {
                new VehicleInfo() { Make = "Audi", Model = "A4", Description = "Lorum ipsum car" },
                new VehicleInfo() { Make = "Audi", Model = "A3", Description = "Lorum ipsum car" },
                new VehicleInfo() { Make = "Audi", Model = "A2", Description = "Lorum ipsum car" }
            };
            return list;
        }
    }
}
