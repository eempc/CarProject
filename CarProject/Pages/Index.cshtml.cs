using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        public List<UserReview> UserReviews { get; set; }

        public async Task OnGetAsync() {
            //UserReviews = await _context.UserReview.Include(u => u.User).ToListAsync(); // Important to note that the .Include( u => u.User) is important if you wish to look up the user via the foreign key
            UserReviews = await _context.UserReview.FromSqlRaw("SELECT * FROM dbo.UserReview")
                //.Where(item => item.Approved == true)        
                .OrderByDescending(item => item.Rating)
                .Take(3)
                .Include(u => u.User)
                .ToListAsync(); // Example of how to use a raw SQL query
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
