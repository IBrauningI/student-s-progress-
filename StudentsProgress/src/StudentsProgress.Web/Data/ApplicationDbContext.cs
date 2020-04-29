using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Data.Identity;

namespace StudentsProgress.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<UserRating> UserRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "AMI-31" },
                new Group { Id = 2, Name = "AMI-32" },
                new Group { Id = 3, Name = "AMI-33" });

            builder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Programming", LecturesCount = 32 },
                new Subject { Id = 2, Name = "Math", LecturesCount = 16 },
                new Subject { Id = 3, Name = "History", LecturesCount = 10 },
                new Subject { Id = 4, Name = "Database", LecturesCount = 32 },
                new Subject { Id = 5, Name = "Start-up", LecturesCount = 10 },
                new Subject { Id = 6, Name = "Cryptology", LecturesCount = 16 });
        }
    }
}
