using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(CommonServices commonServices, AssignmentService assignmentService, UserManager<IdentityUser> userManager)
        {
            _commonServices = commonServices;
            _assignmentService = assignmentService;
            _userManager = userManager;
        }

        public int UserID { get; set; }

        public IdentityUser CurrentUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Get User
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                CurrentUser = user;
            }
            
            PopulateDropDowns(CurrentUser.Id);

            return Page();
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDowns(Assignment.AppUserID);
                ViewData["ErrorMessage"] = "Please correct the errors.";
            }
          
            var assignment = _assignmentService.AddAssignment(Assignment, out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                PopulateDropDowns(Assignment.AppUserID);
                ViewData["ErrorMessage"] = errorMessage;
                return Page();
            }
            else
            {
                return RedirectToPage("./Index");
            }
        }

        private void PopulateDropDowns(string userID)
        {
            ViewData["CourseId"] = _commonServices.GetCoursesSelectList(userID);
            ViewData["SubjectId"] = _commonServices.GetSubjectsSelectList(userID);
            ViewData["Status"] = _commonServices.GetStatusSelectList();
            ViewData["Priority"] = _commonServices.GetPrioritySelectList();
        }   
    }
}
