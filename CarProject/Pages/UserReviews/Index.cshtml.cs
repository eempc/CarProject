using CarProject.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarProject {
    public class UserReviewIndexModel : PageModel {
        private readonly CarProjectContext _context;

        public UserReviewIndexModel(CarProjectContext context) {
            _context = context;
        }

        public IList<UserReview> UserReview { get; set; }

        public async Task OnGetAsync() {
            UserReview = await _context.UserReview
                .Include(u => u.User).ToListAsync();
        }
    }
}
