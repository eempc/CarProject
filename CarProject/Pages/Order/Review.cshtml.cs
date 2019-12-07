using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CarProject.Models;
using Microsoft.AspNetCore.Http;
using CarProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CarProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CarProject {
    [Authorize]
    public class ReviewModel : PageModel {
        private readonly CarProjectContext _context;
        private readonly UserManager<CarProjectUser> _userManager;

        public ReviewModel(CarProjectContext context, UserManager<CarProjectUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        //public DateTime Session_StartDate { get; set; }
        //public DateTime Session_EndDate { get; set; }
        //public string Session_VehicleId { get; set; }

        [BindProperty]
        public Booking NewBooking { get; set; }
        [BindProperty]
        public Vehicle Vehicle { get; set; }
        [BindProperty]
        public CarProjectUser CurrentUser { get; set; }

        public int days;

        public async Task<IActionResult> OnGetAsync() {
            NewBooking = new Booking();

            // Assign the logged in user
            NewBooking.OwnerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            CurrentUser = await _userManager.GetUserAsync(HttpContext.User);

            // Retrieve cookie data such as user start/end date and the desired vehicle GuID
            NewBooking.BookingStartDateTime = DateTime.Parse(HttpContext.Session.GetString("Start date"));
            NewBooking.BookingEndDateTime = DateTime.Parse(HttpContext.Session.GetString("End date"));                    
            Guid vehicleId = Guid.Parse(HttpContext.Session.GetString("Vehicle ID"));
            NewBooking.VehicleId = vehicleId;

            // // Find the vehicle to get its properties like rate, etc...
            if (vehicleId == null) {
                return NotFound();
            }

            Vehicle = await _context.Vehicle.FirstOrDefaultAsync(m => m.VehicleId == vehicleId);

            if (Vehicle == null) {
                return NotFound();
            }

            // Calculate other data
            days = Convert.ToInt32((NewBooking.BookingEndDateTime - NewBooking.BookingStartDateTime).TotalDays + 1);
            NewBooking.PricePaid = days * Vehicle.Rate;

            

            return Page();
        }

        //public async Task<IActionResult> OnPostAsync() {
                   
        //}
    }
}