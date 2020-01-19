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

// Review your order page before pressing submit
namespace CarProject {
    [Authorize]
    public class ReviewModel : PageModel {
        private readonly CarProjectContext _context;
        private readonly UserManager<CarProjectUser> _userManager; // user ID stuff

        public ReviewModel(CarProjectContext context, UserManager<CarProjectUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Booking NewBooking { get; set; }

        public Vehicle Vehicle { get; set; }
        public CarProjectUser CurrentUser { get; set; }
        public double days; // booking number of days requested

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync() {
            NewBooking = new Booking {
                OwnerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value, // standard method for retrieving the id of the user
                BookingStartDateTime = DateTime.Parse(HttpContext.Session.GetString("Start date")), // from cookie
                BookingEndDateTime = DateTime.Parse(HttpContext.Session.GetString("End date")), // from cookie
                VehicleId = Guid.Parse(HttpContext.Session.GetString("Vehicle ID")), // from cookie
            };

            // Assign the logged in user (two methods of getting the user id)

            CurrentUser = await _userManager.GetUserAsync(HttpContext.User); // This is needed for the razor page

            // Retrieve cookie data such as desired start/end date and the desired vehicle GuID

            // Retrieve vehicle
            //Guid vehicleId = Guid.Parse(HttpContext.Session.GetString("Vehicle ID"));
            //NewBooking.VehicleId = vehicleId;

            // // Find the vehicle to get its properties like rate, etc...
            if (NewBooking.VehicleId == null) {
                return NotFound();
            }

            Vehicle = await _context.Vehicle.FirstOrDefaultAsync(m => m.VehicleId == NewBooking.VehicleId);

            if (Vehicle == null) {
                return NotFound();
            }

            // Calculate price (has to be last)
            days = (NewBooking.BookingEndDateTime - NewBooking.BookingStartDateTime).TotalDays + 1;
            NewBooking.PricePaid = (decimal)days * Vehicle.Rate;

            return Page();
        }

        // Basically like the scaffolded Create page
        public async Task<IActionResult> OnPostAsync() {
            NewBooking.DateCreated = DateTime.Now;
            NewBooking.PaymentConfirmed = true;

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Booking.Add(NewBooking);
            await _context.SaveChangesAsync();

            CurrentUser = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); // For some reason this need to be repeated, it's to do with the state
            Message = $"Your order number is {NewBooking.BookingId.ToString()} and a confirmation has been sent to {CurrentUser.Email.ToString()}";

            return RedirectToPage("./Confirm");
        }
    }
}