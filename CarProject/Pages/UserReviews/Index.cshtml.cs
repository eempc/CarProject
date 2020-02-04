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
    public class UserReviewIndexModel : PageModel
    {
        private readonly CarProject.Data.CarProjectContext _context;

        public UserReviewIndexModel(CarProject.Data.CarProjectContext context)
        {
            _context = context;
        }

        public IList<UserReview> UserReview { get;set; }

        public async Task OnGetAsync()
        {
            UserReview = await _context.UserReview
                .Include(u => u.User).ToListAsync();
        }
    }
}
