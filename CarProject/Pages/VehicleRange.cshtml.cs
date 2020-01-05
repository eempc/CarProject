using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CarProject.Data;

namespace CarProject
{
    public class VehicleRangeModel : PageModel
    {

        [BindProperty]
        public List<VehicleInfo> Vehicles { get; set; }

        public VehicleRangeModel() {
            Vehicles = VehicleTypes.GetVehicleTypes();
        }

        


        public void OnGet()
        {

        }
    }
}