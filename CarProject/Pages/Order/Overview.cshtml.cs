using CarProject.Areas.Identity.Data;
using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarProject {
    public class OverviewModel : PageModel {
        private readonly CarProject.Data.CarProjectContext _context;


        public OverviewModel(CarProject.Data.CarProjectContext context) {
            _context = context;
        }

        [BindProperty]
        public Booking NewBooking { get; set; }

        [BindProperty]
        public Vehicle Vehicle { get; set; }


        public double days;

        public async Task OnGetAsync() {
            NewBooking = new Booking();

            // Retrieve vehicle
            Guid vehicleId = Guid.Parse(HttpContext.Session.GetString("Vehicle ID"));
            NewBooking.VehicleId = vehicleId;

            // // Find the vehicle to get its properties like rate, etc...


            Vehicle = await _context.Vehicle.FirstOrDefaultAsync(m => m.VehicleId == vehicleId);

            // Assign the logged in user (two methods of getting the user id)
            NewBooking.OwnerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Retrieve cookie data such as desired start/end date and the desired vehicle GuID
            NewBooking.BookingStartDateTime = DateTime.Parse(HttpContext.Session.GetString("Start date"));
            NewBooking.BookingEndDateTime = DateTime.Parse(HttpContext.Session.GetString("End date"));




            // Calculate price (has to be last)
            days = (NewBooking.BookingEndDateTime - NewBooking.BookingStartDateTime).TotalDays + 1;
            NewBooking.PricePaid = (decimal)days * Vehicle.Rate;


        }


    }
}