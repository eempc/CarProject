using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarProject.Data;
using CarProject.Models;

namespace CarProject
{
    public class UserReviewEditModel : PageModel
    {
        private readonly CarProject.Data.CarProjectContext _context;

        public UserReviewEditModel(CarProject.Data.CarProjectContext context)
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
           ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserReviewExists(UserReview.ReviewId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserReviewExists(int id)
        {
            return _context.UserReview.Any(e => e.ReviewId == id);
        }
    }
}
