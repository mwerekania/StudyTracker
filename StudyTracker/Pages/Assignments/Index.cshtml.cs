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
    public class IndexModel : PageModel
    {
        private readonly AssignmentService _assignmentService;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(AssignmentService assignmentService, UserManager<IdentityUser> userManager)
        {
            _assignmentService = assignmentService;
            _userManager = userManager;
        }

        public int UserID { get; set; }

        public IdentityUser CurrentUser { get; set; } = default!;

        public IList<Assignment> Assignment { get;set; } = default!;


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


            Assignment = _assignmentService.GetAllAssignmentsByUserId(CurrentUser.Id, out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewData["ErrorMessage"] = errorMessage;
            }

            return Page();
        }
    }
}
