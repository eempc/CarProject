using CarProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CarProject.Areas.Identity.Pages.Account.Manage {
    public partial class IndexModel : PageModel {
        private readonly UserManager<CarProjectUser> _userManager;
        private readonly SignInManager<CarProjectUser> _signInManager;

        public IndexModel(
            UserManager<CarProjectUser> userManager,
            SignInManager<CarProjectUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {
            [Required, DataType(DataType.Text), Display(Name = "Full name")]
            public string Name { get; set; }
            [Required, DataType(DataType.Date), Display(Name = "Date of Birth")]
            public DateTime DateOfBirth { get; set; }
            [Required, Display(Name = "Address line 1")]
            public string AddressLine1 { get; set; }
            [Display(Name = "Address line 2")]
            public string AddressLine2 { get; set; }
            [Required]
            public string City { get; set; }
            [Required, DataType(DataType.PostalCode)]
            public string Postcode { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(CarProjectUser user) {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel {
                Name = user.Name,
                DateOfBirth = user.DateOfBirth,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                City = user.City,
                Postcode = user.Postcode,

                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync() {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid) {
                await LoadAsync(user);
                return Page();
            }

            if (Input.Name != user.Name) user.Name = Input.Name;
            if (Input.DateOfBirth != user.DateOfBirth) user.DateOfBirth = Input.DateOfBirth;
            if (Input.AddressLine1 != user.AddressLine1) user.AddressLine1 = Input.AddressLine1;
            if (Input.AddressLine2 != user.AddressLine2) user.AddressLine2 = Input.AddressLine2;
            if (Input.City != user.City) user.City = Input.City;
            if (Input.Postcode != user.Postcode) user.Postcode = Input.Postcode;

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber) {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded) {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
