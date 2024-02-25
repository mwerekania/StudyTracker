using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyTracker.Models;
using StudyTracker.Services;

namespace StudyTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly AssignmentService _assignmentService;

        public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> userManager, AssignmentService assignmentService)
        {
            _logger = logger;
            _userManager = userManager;
            _assignmentService = assignmentService;
        }

        public IdentityUser CurrentUser { get; set; } = default!;

        public IList<Assignment> Assignments { get; set; } = default!;
        public IList<Assignment> UpcomingAssignments { get; set; } = default!;

        public IList<Assignment> CompletedAssignments { get; set; } = default!;

        public IList<Assignment> InProgressAssignments { get; set; } = default!;

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


            Assignments = _assignmentService.GetAllAssignmentsByUserId(CurrentUser.Id, out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewData["ErrorMessage"] = errorMessage;
            }


            UpcomingAssignments = Assignments.Count > 0 ? Assignments.Where(a => a.Status == Status.NotStarted).ToList() : new List<Assignment>();

            CompletedAssignments = Assignments.Count > 0 ? Assignments.Where(a => a.Status == Status.Completed).ToList() : new List<Assignment>();

            InProgressAssignments = Assignments.Count > 0 ? Assignments.Where(a => a.Status == Status.InProgress).ToList() : new List<Assignment>();

            return Page();
        }
    }
}
