using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FFETech.Demo.IdentityServer.RazorUI
{
    public class LogoutModel : PageModel
    {
        #region Public Methods

        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        #endregion
    }
}