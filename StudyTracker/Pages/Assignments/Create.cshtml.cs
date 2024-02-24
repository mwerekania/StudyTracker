using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;

namespace StudyTracker.Pages.Assignments
{
    public class CreateModel : PageModel
    {
        private readonly StudyTrackerDbContext _context;

        private readonly CommonServices _commonServices;
        private readonly AssignmentService _assignmentService;

        public CreateModel(StudyTrackerDbContext context, CommonServices commonServices, AssignmentService assignmentService)
        {
            _context = context;
            _commonServices = commonServices;
            _assignmentService = assignmentService;
        }

        public int UserID { get; set; }

        public IActionResult OnGet()
        {
            UserID = 1002; // Replace with the current user's ID
            PopulateDropDowns();

            return Page();
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            PopulateDropDowns();
            ViewData["ErrorMessage"] = "Please correct the errors.";

            _assignmentService.AddAssignment(Assignment, out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                PopulateDropDowns();
                ViewData["ErrorMessage"] = errorMessage;
                return Page();
            }
            else
            {
                return RedirectToPage("./Index");
            }
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
