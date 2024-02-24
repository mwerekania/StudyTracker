using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;

namespace StudyTracker.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly CourseService _courseService;

        public IndexModel( CourseService courseService)
        {
            _courseService = courseService;
        }

        public int UserID { get; set; }

        public IList<Course> Course { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Get the current user's ID
            UserID = 1002; // Replace with the current user's ID

            Course = _courseService.GetCoursesByUserIDAsync(UserID).Result;

        }
    }
}
