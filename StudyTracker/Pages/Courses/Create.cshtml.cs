using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;

namespace StudyTracker.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly CourseService _courseService;

        public CreateModel(CourseService courseService)
        {
            _courseService = courseService;
        }
        public int UserID { get; set; }

        public IActionResult OnGet()
        {
            UserID = 1002; // Replace with the current user's ID
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Course = _courseService.AddCourse(Course.CourseName, Course.UserId);

            return RedirectToPage("./Index");
        }
    }
}
