using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarProject {
    public class InquiryModel : PageModel {
        [TempData]
        public string TempMessage { get; set; }

        // To do with the start/end dates from Session
        [BindProperty]
        public Inquiry Inquiry { get; set; }
        
        public int TotalDays { get; set; } // Calculated number of days inclusive of start and end

        // These are for the session cookie
        public string Session_StartDate { get; set; }
        public string Session_EndDate { get; set; }

        // Standard database context
        private readonly CarProjectContext _context;

        // This is to determine the path of static files
        private IWebHostEnvironment _env;
        public string webroot;
      
        public InquiryModel(CarProjectContext context, IWebHostEnvironment env) {
            _context = context;
            _env = env;
            webroot = _env.WebRootPath;
        }

        public void SetNewSessionCookie() {
            HttpContext.Session.SetString("Start date", Inquiry.StartDate.ToString());
            HttpContext.Session.SetString("End date", Inquiry.EndDate.ToString());
        }

        public void GetCookieSessionValues() {
            Session_StartDate = HttpContext.Session.GetString("Start date");
            Session_EndDate = HttpContext.Session.GetString("End date");
        }

        public async Task OnGetAsync() {            
            GetCookieSessionValues();

            Inquiry = new Inquiry {
                StartDate = DateTime.Parse(Session_StartDate),
                EndDate = DateTime.Parse(Session_EndDate)
            };

            // This is a check for null entries just in case
            if (string.IsNullOrEmpty(Session_StartDate) 
                || string.IsNullOrEmpty(Session_EndDate)
                || DateTime.Parse(Session_EndDate) < DateTime.Parse(Session_StartDate)
                ) {
                HttpContext.Session.SetString("Start date", DateTime.Today.AddDays(2).ToString());
                HttpContext.Session.SetString("End date", DateTime.Today.AddDays(3).ToString());
            }

            RefreshPageDetails();
        }

        // Button for if the user updates their chosen dates but does not proceed to next step
        public void OnPostUpdate() {
            if (Inquiry.EndDate >= Inquiry.StartDate) {          
                HttpContext.Session.SetString("Start date", Inquiry.StartDate.ToString());
                HttpContext.Session.SetString("End date", Inquiry.EndDate.ToString());
                RefreshPageDetails();
            }
        }

        public void OnPostChooseSize(Size size) {
            RefreshPageDetails();
            Matches = Matches.Where(v => v.Size == size).ToList();
        }

        // If the user proceeds to the next step of order confirmation
        public IActionResult OnPostNext() {
            // More checks in here before proceeding
            if (Inquiry.DesiredVehicleId == null) {
                return Page();
            }

            HttpContext.Session.SetString("Vehicle ID", Inquiry.DesiredVehicleId);

            return RedirectToPage("./Review");
        }

        // Load up two lists, the vehicles and the bookings
        public IList<Vehicle> Vehicles { get; set; }
        public IList<Booking> Bookings { get; set; }
        // Return a list of vacant car matches 
        public IList<Vehicle> Matches { get; set; }

        public void RefreshPageDetails() {
            // Stuff to do with the date
            GetCookieSessionValues();

            TotalDays = Convert.ToInt32((Inquiry.EndDate - Inquiry.StartDate).TotalDays + 1);

            // This must be called again during the update refresh, async is causing problems
            Vehicles = _context.Vehicle.ToList();
            Bookings = _context.Booking.ToList();

            // Method 2 successful but cumbersome
            var vehiclesBooked = from b in Bookings
                                 where
                                    ((this.Inquiry.StartDate >= b.BookingStartDateTime) && (this.Inquiry.StartDate <= b.BookingEndDateTime)) ||
                                    ((this.Inquiry.EndDate >= b.BookingStartDateTime) && (this.Inquiry.EndDate <= b.BookingEndDateTime)) ||
                                    ((this.Inquiry.StartDate <= b.BookingStartDateTime) && (this.Inquiry.EndDate >= b.BookingStartDateTime) && (this.Inquiry.EndDate <= b.BookingEndDateTime)) ||
                                    ((this.Inquiry.StartDate >= b.BookingStartDateTime) && (this.Inquiry.StartDate <= b.BookingEndDateTime) && (this.Inquiry.EndDate >= b.BookingEndDateTime)) ||
                                    ((this.Inquiry.StartDate <= b.BookingStartDateTime) && (this.Inquiry.EndDate >= b.BookingEndDateTime))
                                 select b;

            Matches = (Vehicles
                .Where(v => !vehiclesBooked.Any(b => b.VehicleId == v.VehicleId)))
                .GroupBy(x => x.Model)
                .Select(y => y.First())
                .OrderBy(z => z.Rate)
                .Take(8)
                .ToList();
        }
    }
}