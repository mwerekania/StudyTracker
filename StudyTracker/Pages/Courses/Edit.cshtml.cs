using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;

namespace StudyTracker.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly CourseService _courseService;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel( CourseService courseService, UserManager<IdentityUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Get User
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseService.GetCourseById(id.Value);

            if (course == null)
            {
                return NotFound();
            }
            Course = course;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var updatedCourse = _courseService.UpdateCourse(Course.CourseId, Course.CourseName);

            return RedirectToPage("./Index");
        }
    }
}