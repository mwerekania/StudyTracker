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
    public class IndexModel : PageModel
    {
        private readonly StudyTracker.Data.StudyTrackerDbContext _context;
        private readonly AssignmentService _assignmentService;

        public IndexModel(StudyTracker.Data.StudyTrackerDbContext context, AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            _context = context;
        }

        public int UserID { get; set; }

        public IList<Assignment> Assignment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Get the current user's ID
            UserID = 1002; // Replace with the current user's ID

            Assignment =  _assignmentService.GetAllAssignmentsByUserId(UserID, out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewData["ErrorMessage"] = errorMessage;
            }



            //Assignment = await _context.Assignments
            //    .Include(a => a.Course)
            //    .Include(a => a.Subject)
            //    .Include(a => a.User).ToListAsync();

            // Get the current user's ID

        }
    }
}
