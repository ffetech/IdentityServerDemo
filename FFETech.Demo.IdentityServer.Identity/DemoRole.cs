using System.Collections.Generic;

namespace FFETech.Demo.IdentityServer.Identity
{
    public class DemoRole
    {
        #region Properties

        public string Id => Name;

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public IEnumerable<DemoUserRole> UserRoles { get; set; }

        public IEnumerable<DemoRoleClaim> RoleClaims { get; set; }

        #endregion
    }
}