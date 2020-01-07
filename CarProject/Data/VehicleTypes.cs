using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarProject.Models;

namespace CarProject.Data {
    public class VehicleTypes {
        public static List<VehicleInfo> GetVehicleTypes() {
            List<VehicleInfo> list = new List<VehicleInfo> {
                new VehicleInfo() { Make = "Audi", Model = "A6", Description = "Lorum ipsum car" },
                new VehicleInfo() { Make = "Audi", Model = "A3", Description = "Lorum ipsum car" },
                new VehicleInfo() { Make = "Audi", Model = "A1", Description = "Lorum ipsum car" },
                new VehicleInfo() { Make = "Ford", Model = "Mondeo", Description = "Lorum ipsum car" },
                new VehicleInfo() { Make = "Honda", Model = "Civic", Description = "Lorum ipsum car" },
                new VehicleInfo() { Make = "Nissan", Model = "Leaf", Description = "Lorum ipsum car" },
            };
            return list;
        }
    }
}
