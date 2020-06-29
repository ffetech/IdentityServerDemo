using System.Threading.Tasks;

using FFETech.Demo.IdentityServer.Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FFETech.Demo.IdentityServer.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        #region Fields

        private readonly SignInManager<DemoUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        #endregion

        #region Constructors

        public LogoutModel(SignInManager<DemoUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<IActionResult> OnGet(string returnUrl = "/")
        {
            return await OnPost(returnUrl);
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }

        #endregion
    }
}