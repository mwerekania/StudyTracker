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

namespace StudyTracker.Pages.Assignments
{
    public class DeleteModel : PageModel
    {
        private readonly StudyTracker.Data.StudyTrackerDbContext _context;
        private readonly AssignmentService _assignmentService;

        public DeleteModel(StudyTracker.Data.StudyTrackerDbContext context, AssignmentService assignmentService)
        {
            _context = context;
            _assignmentService = assignmentService;
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Course)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);

            if (assignment == null)
            {
                return NotFound();
            }
            else
            {
                Assignment = assignment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = _assignmentService.DeleteAssignment(id.Value, out string errorMessage, out bool isSuccess);

            if (!isSuccess)
            {
                ViewData["ErrorMessage"] = errorMessage;
                return Page();
            }
            else
            {
                return RedirectToPage("./Index");
            }

            /*
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment != null)
            {
                Assignment = assignment;
                _context.Assignments.Remove(Assignment);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
            */

        }
    }
}
