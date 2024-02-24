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

namespace StudyTracker.Pages.Subjects
{
    public class EditModel : PageModel
    {
        private readonly SubjectService _subjectService;
        private readonly CommonServices _commonServices;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(SubjectService subjectService, CommonServices commonServices, UserManager<IdentityUser> userManager)
        {
            _subjectService = subjectService;
            _commonServices = commonServices;
            _userManager = userManager;
        }

        [BindProperty]
        public Subject Subject { get; set; } = default!;

        public string UserId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                UserId = user.Id;
            }

            if (id == null)
            {
                return NotFound();
            }

            var subject = _subjectService.GetSubjectById(id.Value, out string errorMessage);

            //var subject =  await _context.Subjects.FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }
            Subject = subject;

            ViewData["CourseId"] = _commonServices.GetCoursesSelectList(UserId.ToString());

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var subject = _subjectService.UpdateSubject(Subject.SubjectId, Subject.CourseId, Subject.SubjectName, out string errorMessage);

            return RedirectToPage("./Index");
        }
    }
}
