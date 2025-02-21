using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext()
        {
        }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<InstructorEntity> Instructors { get; set; }
        public DbSet<EnrollmentEntity> Enrollments { get; set; }
        public DbSet<ExamEntity> Exams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var db = System.IO.Path.Join(path, "university.db");
            options.UseSqlite($"Data Source={db}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstructorEntity>().HasData(
                new InstructorEntity() { Id = 1, Name = "Konrad Ogłaza", AcademicTitle = "mgr inż." }
            );
            modelBuilder.Entity<CourseEntity>().HasData(
                new CourseEntity() { Id = 1, Name = "ASP.NET", Credits = 10, InstructorId = 1 }
            );
        }
    }
}