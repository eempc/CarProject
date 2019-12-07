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
        public Inquiry Inquiry { get; set; }

        [BindProperty]
        public int TotalDays { get; set; } // Calculated number of days inclusive of start and end

        // These are for the session cookie
        public string Session_StartDate { get; set; }
        public string Session_EndDate { get; set; }

        // Standard database context
        private readonly CarProjectContext _context;

        // Load up two lists, the vehicles and the bookings
        public IList<Vehicle> Vehicles { get; set; }
        public IList<Booking> Bookings { get; set; }

        public InquiryModel(CarProjectContext context) {
            _context = context;
        }

        public async Task OnGetAsync() {
            // Must be called over and over and over! This is not the efficient part
            Vehicles = await _context.Vehicle.ToListAsync();
            Bookings = await _context.Booking.ToListAsync();

            Session_StartDate = HttpContext.Session.GetString("Start date");
            Session_EndDate = HttpContext.Session.GetString("End date");

            if (!string.IsNullOrEmpty(Session_StartDate) && !string.IsNullOrEmpty(Session_EndDate)) {
                RefreshPageDetails();
            }
        }

        // Button If the user only updates their chosen dates but does not proceed to next step
        public IActionResult OnPostUpdate() {
            HttpContext.Session.SetString("Start date", Inquiry.StartDate.ToString());
            HttpContext.Session.SetString("End date", Inquiry.EndDate.ToString());

            RefreshPageDetails();

            return Page();
        }

        // If the user proceeds to the next step
        public IActionResult OnPostNext() {
            HttpContext.Session.SetString("Start date", Inquiry.StartDate.ToString());
            HttpContext.Session.SetString("End date", Inquiry.EndDate.ToString());
            HttpContext.Session.SetString("Vehicle ID", Inquiry.DesiredVehicleId.ToString());
            
            return RedirectToPage("./Review");
        }

        // Return a list of vacant car matches
        public IList<Vehicle> Matches { get; set; }

        public void RefreshPageDetails() {
            // Stuff to do with the date
            Session_StartDate = HttpContext.Session.GetString("Start date");
            Session_EndDate = HttpContext.Session.GetString("End date");

            Inquiry = new Inquiry {
                StartDate = DateTime.Parse(Session_StartDate),
                EndDate = DateTime.Parse(Session_EndDate)
            };

            TotalDays = Convert.ToInt32((Inquiry.EndDate - Inquiry.StartDate).TotalDays + 1);

            // This must be called again during the update refresh, async is causing problems
            Vehicles = _context.Vehicle.ToList();
            Bookings = _context.Booking.ToList();

            // Method 1 failed
            //Matches = Vehicles.Where(vehicle => Bookings.All(booking => booking.BookingEndDateTime < StartDate || booking.BookingStartDateTime > EndDate)).ToList();

            // Method 2 successful but cumbersome
            var vehiclesBooked = from b in Bookings
                                 where
                                    ((this.Inquiry.StartDate >= b.BookingStartDateTime) && (this.Inquiry.StartDate <= b.BookingEndDateTime)) ||
                                    ((this.Inquiry.EndDate >= b.BookingStartDateTime) && (this.Inquiry.EndDate <= b.BookingEndDateTime)) ||
                                    ((this.Inquiry.StartDate <= b.BookingStartDateTime) && (this.Inquiry.EndDate >= b.BookingStartDateTime) && (this.Inquiry.EndDate <= b.BookingEndDateTime)) ||
                                    ((this.Inquiry.StartDate >= b.BookingStartDateTime) && (this.Inquiry.StartDate <= b.BookingEndDateTime) && (this.Inquiry.EndDate >= b.BookingEndDateTime)) ||
                                    ((this.Inquiry.StartDate <= b.BookingStartDateTime) && (this.Inquiry.EndDate >= b.BookingEndDateTime))
                                 select b;

            Matches = (Vehicles.Where(v => !vehiclesBooked.Any(b => b.VehicleId == v.VehicleId))).ToList();
        }

    }
}