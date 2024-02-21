using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;

namespace StudyTracker.Pages.Assignments
{
    public class EditModel : PageModel
    {
        private readonly StudyTracker.Data.StudyTrackerDbContext _context;

        private readonly CommonServices _commonServices;
        private readonly AssignmentService _assignmentService;


        public EditModel(StudyTracker.Data.StudyTrackerDbContext context, CommonServices commonServices, AssignmentService assignmentService)
        {
            _context = context;
            _commonServices = commonServices;
            _assignmentService = assignmentService;
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = default!;

        public int UserID { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UserID = 1002; // Replace with the current user's ID

            if (id == null)
            {
                return NotFound();
            }

            var assignment =  await _context.Assignments.FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (assignment == null)
            {
                return NotFound();
            }
            Assignment = assignment;

            PopulateDropDowns();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDowns();
                //return Page();
            }

            _context.Attach(Assignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(Assignment.AssignmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.AssignmentId == id);
        }


        private void PopulateDropDowns()
        {
            ViewData["CourseId"] = _commonServices.GetCoursesSelectList(UserID);
            ViewData["SubjectId"] = _commonServices.GetSubjectsSelectList();
            ViewData["Status"] = _commonServices.GetStatusSelectList();
            ViewData["Priority"] = _commonServices.GetPrioritySelectList();
        }
    }
}
