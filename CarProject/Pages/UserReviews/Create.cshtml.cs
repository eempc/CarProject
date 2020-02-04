using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarProject.Data;
using CarProject.Models;

namespace CarProject {
    public class UserReviewCreateModel : PageModel {
        private readonly CarProject.Data.CarProjectContext _context;

        public UserReviewCreateModel(CarProject.Data.CarProjectContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public UserReview UserReview { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.UserReview.Add(UserReview);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
