using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Models;

namespace StudyTracker.Services
{
    public class CourseService
    {
        private readonly StudyTrackerDbContext _dbcontext;

        public CourseService(StudyTrackerDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public Course AddCourse(string courseName, string userId)
        {
            Course course = new Course
            {
                CourseName = courseName,
                AppUserID = userId,
                DateAdded = DateTime.Now
            };

            _dbcontext.Courses.Add(course);
            _dbcontext.SaveChanges();

            return course;
        }

        public Course GetCourseById(int courseId)
        {
            return _dbcontext.Courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _dbcontext.Courses.ToList();
        }


        public async Task<IList<Course>> GetCoursesByUserIDAsync(string? userID = null)
        {
            var courses = await _dbcontext.Courses
                .Where(c => c.AppUserID == userID && c.DateDeleted == null)
                .ToListAsync();
            return courses;
        }



        public Course UpdateCourse(int courseId, string courseName)
        {
            Course course = _dbcontext.Courses.FirstOrDefault(c => c.CourseId == courseId);

            if (course != null)
            {
                course.CourseName = courseName;
                course.DateModified = DateTime.Now;

                _dbcontext.SaveChanges();
            }

            return course;
        }

        public bool DeleteCourse(int courseId)
        {
            Course course = _dbcontext.Courses.FirstOrDefault(c => c.CourseId == courseId);

            if (course != null)
            {
                course.DateDeleted = DateTime.Now;
                _dbcontext.SaveChanges();
                return true;
            }

            return false;
        }

    }
}
