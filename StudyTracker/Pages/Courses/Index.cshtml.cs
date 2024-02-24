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
    public class IndexModel : PageModel
    {
        private readonly CourseService _courseService;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel( CourseService courseService, UserManager<IdentityUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }

        public string UserID { get; set; }

        public IList<Course> Course { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Get User
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            UserID = user.Id;

            Course = _courseService.GetCoursesByUserIDAsync(UserID).Result;
            return Page();
        }
    }
}
