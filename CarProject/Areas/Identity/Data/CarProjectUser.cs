using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarProject.Areas.Identity.Data {
    // Add profile data for application users by adding properties to the CarProjectUser class
    public class CarProjectUser : IdentityUser
    {
        [PersonalData, Required]
        public string Name { get; set; }
        [PersonalData, Required, DataType(DataType.Date), Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [PersonalData, Required, Display(Name = "Address line 1")]
        public string AddressLine1 { get; set; }
        [PersonalData, Display(Name = "Address line 2")]
        public string AddressLine2 { get; set; }
        [PersonalData, Required]
        public string City { get; set; }
        [PersonalData, Required, DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
