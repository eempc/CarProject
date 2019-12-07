using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarProject {
    public class VehicleIndexModel : PageModel {
        private readonly CarProjectContext _context;

        public VehicleIndexModel(CarProjectContext context) {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get; set; }

        public async Task OnGetAsync() {
            Vehicle = await _context.Vehicle.ToListAsync();
            //Vehicle = await _context.Database.ExecuteSqlCommandAsync("SELECT * FROM Vehicle").ToListAsync();
        }
    }
}
