using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using FFETech.Demo.IdentityServer.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FFETech.Demo.IdentityServer.Pages.Account
{
    public class DeletePersonalDataModel : PageModel
    {
        #region Nested Types

        public class InputModel
        {
            #region Properties

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            #endregion
        }

        #endregion

        #region Fields

        private readonly UserManager<DemoUser> _userManager;
        private readonly SignInManager<DemoUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;

        #endregion

        #region Constructors

        public DeletePersonalDataModel(
            UserManager<DemoUser> userManager,
            SignInManager<DemoUser> signInManager,
            ILogger<DeletePersonalDataModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #endregion

        #region Properties

        [BindProperty]
        public InputModel Input { get; set; }

        public bool RequirePassword { get; set; }

        #endregion

        #region Public Methods

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }

        #endregion
    }
}