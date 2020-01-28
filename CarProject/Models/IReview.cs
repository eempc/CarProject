using CarProject.Areas.Identity.Data;
using System;

namespace CarProject.Models {
    interface IReview {
        int ReviewId { get; set; }
        DateTime DateCreated { get; set; }
        string OwnerId { get; set; }
        CarProjectUser User { get; set; }
        string ReviewTitle { get; set; }
        string ReviewDescription { get; set; }
        int Rating { get; set; }
    }
}
