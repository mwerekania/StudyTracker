using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudyTracker.Common;
using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;

namespace StudyTracker.Pages.Subjects
{
    public class CreateModel : PageModel
    {
        private readonly CommonServices _commonServices; 
        private readonly SubjectService _subjectService;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(SubjectService subjectService, CommonServices commonServices, UserManager<IdentityUser> userManager)
        {
            _subjectService = subjectService;
            _commonServices = commonServices;
            _userManager = userManager;
        }
        public IdentityUser CurrentUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                CurrentUser = user;
            }

            ViewData["CourseId"] = _commonServices.GetCoursesSelectList(user.Id);
            return Page();
        }

        [BindProperty]
        public Subject Subject { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Subject = _subjectService.AddSubject(Subject.SubjectName, Subject.CourseId, Subject.AppUserID, out string errorMessage);

            return RedirectToPage("./Index");
        }
    }
}
