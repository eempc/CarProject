using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CarProject.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public Inquiry Inquiry { get; set; }

        public IndexModel(ILogger<IndexModel> logger) {
            _logger = logger;
        }

        public void OnGet() {

        }
    }
}
