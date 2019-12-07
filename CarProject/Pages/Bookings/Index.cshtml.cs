using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarProject.Data;
using CarProject.Models;

namespace CarProject
{
    public class BookingIndexModel : PageModel
    {
        private readonly CarProject.Data.CarProjectContext _context;

        public BookingIndexModel(CarProject.Data.CarProjectContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; }

        public async Task OnGetAsync()
        {
            Booking = await _context.Booking
                .Include(b => b.User)
                .Include(b => b.Vehicle).ToListAsync();
        }
    }
}
