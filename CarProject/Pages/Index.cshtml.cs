using CarProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CarProject.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public Inquiry Inquiry { get; set; }

        public IndexModel(ILogger<IndexModel> logger) {
            _logger = logger;
        }

        public void OnGet() {

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
