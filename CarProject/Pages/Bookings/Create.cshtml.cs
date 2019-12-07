using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarProject {
    [Authorize]
    public class BookingCreateModel : PageModel {
        private readonly CarProjectContext _context;

        public BookingCreateModel(CarProjectContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "RegistrationMark");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            Booking.DateCreated = DateTime.Now;
            Booking.OwnerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
