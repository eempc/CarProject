using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarProject.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private readonly CarProjectContext _context; // database context

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public Inquiry Inquiry { get; set; }

        public IndexModel(ILogger<IndexModel> logger, CarProjectContext context) {
            _logger = logger;
            _context = context;
        }

        public IList<UserReview> UserReviews { get; set; }

        public async Task OnGetAsync() {
            UserReviews = await _context.UserReview
                .Include(u => u.User).ToListAsync();
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid || Inquiry.EndDate < Inquiry.StartDate) {
                ErrorMessage = "Invalid date(s)";
                return Page();
            }

            HttpContext.Session.SetString("Start date", Inquiry.StartDate.ToString());
            HttpContext.Session.SetString("End date", Inquiry.EndDate.ToString());

            return RedirectToPage("./Order/Inquiry");
        }
    }
}
