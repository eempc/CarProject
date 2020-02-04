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
    public class UserReviewDeleteModel : PageModel
    {
        private readonly CarProject.Data.CarProjectContext _context;

        public UserReviewDeleteModel(CarProject.Data.CarProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserReview UserReview { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserReview = await _context.UserReview
                .Include(u => u.User).FirstOrDefaultAsync(m => m.ReviewId == id);

            if (UserReview == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserReview = await _context.UserReview.FindAsync(id);

            if (UserReview != null)
            {
                _context.UserReview.Remove(UserReview);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
