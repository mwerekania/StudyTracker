using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudyTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

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

            return Page();
        }
    }
}
