using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CarProject.Models;
using Microsoft.AspNetCore.Http;

namespace CarProject {
    public class ReviewModel : PageModel {
        [BindProperty]
        public Inquiry Inquiry { get; set; }

        public string Session_StartDate { get; set; }
        public string Session_EndDate { get; set; }
        public string Session_VehicleId { get; set; }

        public void OnGet() {
            Session_StartDate = HttpContext.Session.GetString("Start date");
            Session_EndDate = HttpContext.Session.GetString("End date");
            Session_VehicleId = HttpContext.Session.GetString("Vehicle ID");
        }
    }
}