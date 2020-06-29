using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FFETech.Demo.IdentityServer.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
        #region Public Methods

        public void OnGet()
        {
        }

        #endregion
    }
}