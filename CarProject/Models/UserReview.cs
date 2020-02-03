﻿using CarProject.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarProject.Models {
    public class UserReview : IUserReview {
        [Required, Key]
        public int ReviewId { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        [Required, ForeignKey("User")]
        public string OwnerId { get; set; }
        public CarProjectUser User { get; set; }
        [Required, MaxLength(48)]
        public string ReviewTitle { get; set; }
        [MaxLength(500)]
        public string ReviewDescription { get; set; }
        [Required, Range(1,5)]
        public int Rating { get; set; }
    }
}
