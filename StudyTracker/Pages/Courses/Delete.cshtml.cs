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
    public class DeleteModel : PageModel
    {
        private readonly CourseService _courseService;
        private readonly StudyTracker.Data.StudyTrackerDbContext _context;

        public DeleteModel(StudyTracker.Data.StudyTrackerDbContext context, CourseService courseService)
        {
            _context = context;
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseService.GetCourseById(id.Value);


            if (course == null)
            {
                return NotFound();
            }
            else
            {
                Course = course;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _courseService.DeleteCourse(id.Value);

            /*
             * var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                Course = course;
                _context.Courses.Remove(Course);
                await _context.SaveChangesAsync();
            }
            */

            return RedirectToPage("./Index");
        }
    }
}
