using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly AssignmentService _assignmentService;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(AssignmentService assignmentService, UserManager<IdentityUser> userManager)
        {
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

        }
    }
}
