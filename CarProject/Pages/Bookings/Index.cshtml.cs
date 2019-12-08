using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System;

namespace CarProject {
    public class BookingIndexModel : PageModel {
        private readonly CarProjectContext _context;
        private string userId;

        public BookingIndexModel(CarProjectContext context) {
            _context = context;
        }

        public IList<Booking> Booking { get; set; }

        public async Task OnGetAsync() {
            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Booking = await _context.Booking
                //.Where(b => b.OwnerId == userId)
                .Include(b => b.User)
                .Include(b => b.Vehicle).ToListAsync();
        }
    }
}
