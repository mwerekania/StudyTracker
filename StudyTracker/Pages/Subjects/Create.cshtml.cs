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

namespace StudyTracker.Pages.Subjects
{
    public class CreateModel : PageModel
    {
        private readonly CommonServices _commonServices; 
        private readonly SubjectService _subjectService;

        public CreateModel(SubjectService subjectService, CommonServices commonServices)
        {
            _subjectService = subjectService;
            _commonServices = commonServices;
        }

        public int UserID { get; set; }

        public IActionResult OnGet()
        {
            // Get the current user's ID
            UserID = 1002; // Replace with the current user's ID

            ViewData["CourseId"] = _commonServices.GetCoursesSelectList(UserID);
            return Page();
        }

        [BindProperty]
        public Subject Subject { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Subject = _subjectService.AddSubject(Subject.SubjectName, Subject.CourseId, out string errorMessage);

            return RedirectToPage("./Index");
        }
    }
}
