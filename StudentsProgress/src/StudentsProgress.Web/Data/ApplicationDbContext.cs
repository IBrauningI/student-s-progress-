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
                new Subject { Id = 1, Name = "Programming" },
                new Subject { Id = 2, Name = "Math" },
                new Subject { Id = 3, Name = "History" },
                new Subject { Id = 4, Name = "Database" },
                new Subject { Id = 5, Name = "Start-up" },
                new Subject { Id = 6, Name = "Cryptology" });

            builder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    UserId = "138ea16f-0bbf-487b-b0f2-c824095d2634",
                    Faculty = "AMI",
                    GroupId = 1,
                });

            builder.Entity<Attendance>().HasData(
                new Attendance { Id = 1, StudentId = 1, SubjectId = 1 },
                new Attendance { Id = 2, StudentId = 1, SubjectId = 2 },
                new Attendance { Id = 3, StudentId = 1, SubjectId = 3 },
                new Attendance { Id = 4, StudentId = 1, SubjectId = 4 },
                new Attendance { Id = 5, StudentId = 1, SubjectId = 5 },
                new Attendance { Id = 6, StudentId = 1, SubjectId = 6 });

            builder.Entity<UserRating>().HasData(
                new UserRating { Id = 1, StudentId = 1, SubjectId = 1 },
                new UserRating { Id = 2, StudentId = 1, SubjectId = 2 },
                new UserRating { Id = 3, StudentId = 1, SubjectId = 3 },
                new UserRating { Id = 4, StudentId = 1, SubjectId = 4 },
                new UserRating { Id = 5, StudentId = 1, SubjectId = 5 },
                new UserRating { Id = 6, StudentId = 1, SubjectId = 6 });
        }
    }
}
