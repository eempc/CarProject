using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarProject {
    [Authorize]
    public class BookingIndexModel : PageModel {
        private readonly CarProjectContext _context;
        //private string userId;

        public BookingIndexModel(CarProjectContext context) {
            _context = context;
        }

        public IList<Booking> Booking { get; set; }

        public async Task OnGetAsync() {
            //userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Booking = await _context.Booking
                //.Where(b => b.OwnerId == userId)
                .Include(b => b.User)
                .Include(b => b.Vehicle).ToListAsync();
        }
    }
}
