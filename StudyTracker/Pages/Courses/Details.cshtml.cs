using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;

namespace StudyTracker.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly CourseService _courseService;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel( CourseService courseService, UserManager<IdentityUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }

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
            else
            {
                Course = course;
            }
            return Page();
        }
    }
}
