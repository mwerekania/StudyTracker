using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Models;

namespace StudyTracker.Services
{
    public class CommonServices
    {
        private readonly StudyTrackerDbContext _dbContext;
        public CommonServices(StudyTrackerDbContext context)
        {
            _dbContext = context;
        }

        public SelectList GetCoursesSelectList()
        {
            return new SelectList(_dbContext.Courses, "CourseId", "CourseName");
        }

        public SelectList GetCoursesSelectList(string userID)
        {
            return new SelectList(_dbContext.Courses.Where(c => c.AppUserID == userID && c.DateDeleted == null), "CourseId", "CourseName");
        }

        public SelectList GetSubjectsSelectList(string userID)
        {
            return new SelectList(_dbContext.Subjects.Where(s => s.AppUserID == userID && s.DateDeleted == null), "SubjectId", "SubjectName");
        }

        public SelectList GetSubjectsSelectList(int courseId)
        {
            return new SelectList(_dbContext.Subjects.Where(s => s.CourseId == courseId && s.DateDeleted == null), "SubjectId", "SubjectName");
        }

        public SelectList GetUsersSelectList()
        {
            return new SelectList(_dbContext.Users, "UserId", "UserName");
        }

        // Add a method to get the status select list
        public SelectList GetStatusSelectList()
        {
            return new SelectList(Enum.GetValues<Status>());
        }

        // Add a method to get the priority select list
        public SelectList GetPrioritySelectList()
        {
            return new SelectList(Enum.GetValues<Priority>());
        }

    }
}
