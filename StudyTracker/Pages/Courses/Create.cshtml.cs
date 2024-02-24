using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(CourseService courseService, UserManager<IdentityUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }
        public string UserID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get User
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            UserID = user.Id;

            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public Task<IActionResult> OnPostAsync()
        {
            Course = _courseService.AddCourse(Course.CourseName, Course.AppUserID);

            return Task.FromResult<IActionResult>(RedirectToPage("./Index"));
        }
    }
}
