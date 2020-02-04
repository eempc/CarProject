using CarProject.Areas.Identity.Data;
using CarProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarProject.Data {
    public class CarProjectContext : IdentityDbContext<CarProjectUser> {
        public CarProjectContext(DbContextOptions<CarProjectContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<Booking> Booking { get; set; }

        public DbSet<CarProject.Models.UserReview> UserReview { get; set; }
    }
}
