using System.Threading.Tasks;

using FFETech.Demo.IdentityServer.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FFETech.Demo.IdentityServer.Pages.Account
{
    public class ResetAuthenticatorModel : PageModel
    {
        #region Fields

        private readonly SignInManager<DemoUser> _signInManager;

        UserManager<DemoUser> _userManager;
        ILogger<ResetAuthenticatorModel> _logger;

        #endregion

        #region Constructors

        public ResetAuthenticatorModel(
            UserManager<DemoUser> userManager,
            SignInManager<DemoUser> signInManager,
            ILogger<ResetAuthenticatorModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #endregion

        #region Properties

        [TempData]
        public string StatusMessage { get; set; }

        #endregion

        #region Public Methods

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your authenticator app key has been reset, you will need to configure your authenticator app using the new key.";

            return RedirectToPage("./EnableAuthenticator");
        }

        #endregion
    }
}