using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarProject
{
    public class VehicleRangeModel : PageModel
    {

        [BindProperty]
        public IList<Vehicle> Vehicles { get; set; }

        public VehicleRangeModel() {

        }

        


        public void OnGet()
        {

        }
    }
}