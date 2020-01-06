using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CarProject.Data;
using Microsoft.AspNetCore.Hosting;

namespace CarProject
{
    public class VehicleRangeModel : PageModel
    {

        [BindProperty]
        public List<VehicleInfo> Vehicles { get; set; }
        // This is to determine the path of static files
        private IWebHostEnvironment _env;
        public string webroot;

        public VehicleRangeModel(IWebHostEnvironment env) {
            Vehicles = VehicleTypes.GetVehicleTypes();
            _env = env;
            webroot = _env.WebRootPath;
        }

        


        public void OnGet()
        {

        }
    }
}