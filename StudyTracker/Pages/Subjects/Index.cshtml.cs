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
    public class IndexModel : PageModel
    {
        private readonly SubjectService _subjectService;

        public IndexModel( SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public int UserID { get; set; }

        public IList<Subject> Subject { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Get the current user's ID
            UserID = 1002; // Replace with the current user's ID

            Subject = (IList<Subject>)_subjectService.GetSubjectsWithCoursesAsync(UserID, out string errorMessage);
        }
    }
}
