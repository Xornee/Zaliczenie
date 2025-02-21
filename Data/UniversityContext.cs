using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UniversityContext : IdentityDbContext<IdentityUser>
    {
        // Constructor required for DI; configuration passed via AddDbContext will be used.
        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<InstructorEntity> Instructors { get; set; }
        public DbSet<EnrollmentEntity> Enrollments { get; set; }
        public DbSet<ExamEntity> Exams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // This configuration will only be applied if the context is created without options (e.g., via CLI migrations).
            if (!options.IsConfigured)
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                var db = System.IO.Path.Join(path, "university.db");
                options.UseSqlite($"Data Source={db}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply Identity configuration first.
            base.OnModelCreating(modelBuilder);

            // Seed data for domain entities.
            modelBuilder.Entity<InstructorEntity>().HasData(
                new InstructorEntity() { Id = 1, Name = "Konrad Ogłaza", AcademicTitle = "mgr inż." }
            );
            modelBuilder.Entity<CourseEntity>().HasData(
                new CourseEntity() { Id = 1, Title = "ASP.NET", Credits = 10, InstructorId = 1 }
            );

            // Seed Identity data.
            string ADMIN_ID = Guid.NewGuid().ToString();
            string ROLE_ID = Guid.NewGuid().ToString();

            // Add admin role.
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = ROLE_ID
            });

            // Create administrator user.
            var admin = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "adminuser@wsei.edu.pl",
                EmailConfirmed = true,
                UserName = "adminuser@wsei.edu.pl",
                NormalizedUserName = "ADMINUSER@WSEI.EDU.PL",
                NormalizedEmail = "ADMINUSER@WSEI.EDU.PL"
            };

            // Hash password.
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = ph.HashPassword(admin, "S3cretPassword");

            // Save the administrator user.
            modelBuilder.Entity<IdentityUser>().HasData(admin);

            // Assign administrator role to the user.
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
