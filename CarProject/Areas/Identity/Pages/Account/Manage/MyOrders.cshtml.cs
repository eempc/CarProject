using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarProject.Areas.Identity.Pages.Account.Manage {
    public class MyOrdersModel : PageModel {
        private readonly CarProjectContext _context;
        private string userId;
        public IList<Booking> Booking { get; set; }

        public MyOrdersModel(CarProjectContext context) {
            _context = context;
        }

        public async Task OnGetAsync() {
            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Booking = await _context.Booking
                .Where(b => b.OwnerId == userId)
                .Include(b => b.User)
                .Include(b => b.Vehicle).ToListAsync();
        }
    }
}
