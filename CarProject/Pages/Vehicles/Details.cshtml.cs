﻿using CarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CarProject {
    public class VehicleDetailsModel : PageModel
    {
        private readonly CarProject.Data.CarProjectContext _context;

        public VehicleDetailsModel(CarProject.Data.CarProjectContext context)
        {
            _context = context;
        }

        public Vehicle Vehicle { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle = await _context.Vehicle.FirstOrDefaultAsync(m => m.VehicleId == id);

            if (Vehicle == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
