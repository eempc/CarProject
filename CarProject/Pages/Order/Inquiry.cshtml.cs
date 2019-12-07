using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using CarProject.Models;
using CarProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CarProject {
    public class InquiryModel : PageModel {
        // To do with the start/end dates from Session
        [BindProperty]
        public Inquiry Inquiry { get; set; } // Contains start/end date
        [BindProperty]
        public int TotalDays { get; set; } // Calculated number of days

        public string Session_StartDate { get; set; }
        public string Session_EndDate { get; set; }

        // See which cars are available for the chosen dates
        private readonly CarProjectContext _context;

        public IList<Vehicle> Vehicle { get; set; }

        public InquiryModel(CarProjectContext context) {
            _context = context;
        }

        public async Task OnGetAsync() {
            Vehicle = await _context.Vehicle.ToListAsync();

            Session_StartDate = HttpContext.Session.GetString("Start date");
            Session_EndDate = HttpContext.Session.GetString("End date");

            if (!string.IsNullOrEmpty(Session_StartDate) && !string.IsNullOrEmpty(Session_EndDate)) {
                RefreshPageDetails();
            }

            
        }


        public IActionResult OnPostUpdate() {
            HttpContext.Session.SetString("Start date", Inquiry.StartDate.ToString());
            HttpContext.Session.SetString("End date", Inquiry.EndDate.ToString());

            RefreshPageDetails();

            return Page();
        }

        public void RefreshPageDetails() {
            // Stuff to do with the date
            Session_StartDate = HttpContext.Session.GetString("Start date");
            Session_EndDate = HttpContext.Session.GetString("End date");

            Inquiry = new Inquiry {
                StartDate = DateTime.Parse(Session_StartDate),
                EndDate = DateTime.Parse(Session_EndDate)
            };
            TotalDays = Convert.ToInt32((Inquiry.EndDate - Inquiry.StartDate).TotalDays + 1);

            // Stuff to do with the cars


        }

    }
}