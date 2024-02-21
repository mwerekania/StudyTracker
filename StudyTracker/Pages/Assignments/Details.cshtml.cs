using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Models;

namespace StudyTracker.Pages.Assignments
{
    public class DetailsModel : PageModel
    {
        private readonly StudyTracker.Data.StudyTrackerDbContext _context;

        public DetailsModel(StudyTracker.Data.StudyTrackerDbContext context)
        {
            _context = context;
        }

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
    }
}
