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

namespace StudyTracker.Pages.Subjects
{
    public class DeleteModel : PageModel
    {
        private readonly SubjectService _subjectService;

        private readonly StudyTracker.Data.StudyTrackerDbContext _context;

        public DeleteModel(StudyTracker.Data.StudyTrackerDbContext context, SubjectService subjectService)
        {
            _subjectService = subjectService;
            _context = context;
        }

        [BindProperty]
        public Subject Subject { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = _subjectService.GetSubjectById(id.Value, out string errorMessage);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = _subjectService.DeleteSubject((int)id, out string errorMessage, out bool isSuccess);

            return RedirectToPage("./Index");
        }
    }
}
