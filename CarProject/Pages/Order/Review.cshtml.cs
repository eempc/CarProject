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
    [Authorize]
    public class ReviewModel : PageModel {
        private readonly CarProjectContext _context;
        private readonly UserManager<CarProjectUser> _userManager;

        public ReviewModel(CarProjectContext context, UserManager<CarProjectUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Booking NewBooking { get; set; }
       
        public Vehicle Vehicle { get; set; }       
        public CarProjectUser CurrentUser { get; set; }
        public double days;

        public async Task<IActionResult> OnGetAsync() {
            NewBooking = new Booking();

            // Assign the logged in user (two methods of getting the user id)
            NewBooking.OwnerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            CurrentUser = await _userManager.GetUserAsync(HttpContext.User);

            // Retrieve cookie data such as desired start/end date and the desired vehicle GuID
            NewBooking.BookingStartDateTime = DateTime.Parse(HttpContext.Session.GetString("Start date"));
            NewBooking.BookingEndDateTime = DateTime.Parse(HttpContext.Session.GetString("End date"));
            
            // Retrieve vehicle
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

            // Calculate price (has to be last)
            days = (NewBooking.BookingEndDateTime - NewBooking.BookingStartDateTime).TotalDays + 1;
            NewBooking.PricePaid = (decimal)days * Vehicle.Rate;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            NewBooking.DateCreated = DateTime.Now;
            NewBooking.PaymentConfirmed = true;

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Booking.Add(NewBooking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Confirm");
        }
    }
}