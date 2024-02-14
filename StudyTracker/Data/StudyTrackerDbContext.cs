using Microsoft.EntityFrameworkCore;
using StudyTracker.Models;
using System.Reflection.Metadata;

namespace StudyTracker.Data
{
    public class StudyTrackerDbContext : DbContext
    {
        public StudyTrackerDbContext()
        {
        }

        public StudyTrackerDbContext(DbContextOptions<StudyTrackerDbContext> options)
         : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Assignment> Assignments { get; set; }

    }
}
