using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudyTracker.Common
{
    public class BasePageModel : PageModel
    {
        protected readonly UserManager<IdentityUser> _userManager;
        public string UserId { get; set; }

        public BasePageModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetUserManagerAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            UserId = user.Id;

            return Page();
        }
    }
}
