using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly CommonServices _commonServices;
        private readonly AssignmentService _assignmentService;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(CommonServices commonServices, AssignmentService assignmentService, UserManager<IdentityUser> userManager)
        {
            _commonServices = commonServices;
            _assignmentService = assignmentService;
            _userManager = userManager;
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = default!;

        public IdentityUser CurrentUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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

            if (id == null)
            {
                return NotFound();
            }

            var assignment = _assignmentService.GetAssignmentById(id.Value, out string errorMessage);

            //var assignment =  await _context.Assignments.FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (assignment == null)
            {
                return NotFound();
            }
            Assignment = assignment;

            PopulateDropDowns(CurrentUser.Id);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDowns(Assignment.AppUserID);
                //return Page();
            }

            var assignment = _assignmentService.UpdateAssignment(Assignment, out string errorMessage);

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

        private void PopulateDropDowns(string userId)
        {
            ViewData["CourseId"] = _commonServices.GetCoursesSelectList(userId);
            ViewData["SubjectId"] = _commonServices.GetSubjectsSelectList(userId);
            ViewData["Status"] = _commonServices.GetStatusSelectList();
            ViewData["Priority"] = _commonServices.GetPrioritySelectList();
        }
    }
}
