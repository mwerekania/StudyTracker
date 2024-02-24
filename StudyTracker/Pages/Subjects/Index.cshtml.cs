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

namespace StudyTracker.Pages.Subjects
{
    public class IndexModel : PageModel
    {
        private readonly SubjectService _subjectService;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel( SubjectService subjectService, UserManager<IdentityUser> userManager)
        {
            _subjectService = subjectService;
            _userManager = userManager;
        }

        public IList<Subject> Subject { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Get User
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            Subject = _subjectService.GetSubjectsWithCoursesAsync(user.Id, out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewData["ErrorMessage"] = errorMessage;
            }

            return Page();
        }
    }
}
