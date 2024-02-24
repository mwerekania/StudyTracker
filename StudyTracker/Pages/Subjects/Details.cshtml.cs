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
    public class DetailsModel : PageModel
    {
        private readonly SubjectService _subjectService;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(SubjectService subjectService, UserManager<IdentityUser> userManager)
        {
            _subjectService = subjectService;
            _userManager = userManager;
        }

        public Subject Subject { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            if (id == null)
            {
                return NotFound();
            }

            var subject = _subjectService.GetSubjectById(id.Value, out string errorMessage);

           // var subject = await _context.Subjects.FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }
            else
            {
                Subject = subject;
            }
            return Page();
        }
    }
}
