using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarProject.Areas.Identity.Pages.Account.Manage {
    [Authorize]
    public class MyReviewModel : PageModel {
        [BindProperty]
        public UserReview UserReview { get; set; }

        private readonly CarProjectContext _context;

        public MyReviewModel(CarProjectContext context) {
            _context = context;
        }
        public void OnGet() {
            UserReview = new UserReview {
                OwnerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            };
        }

        public async Task<IActionResult> OnPostAsync() {
            UserReview.DateCreated = DateTime.Now;

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.UserReview.Add(UserReview);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
