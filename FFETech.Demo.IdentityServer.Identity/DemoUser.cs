using System;
using System.Collections.Generic;

namespace FFETech.Demo.IdentityServer.Identity
{
    public class DemoUser
    {
        #region Properties

        public string Id => UserName;

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTime? LockoutEnd { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string SecurityStamp { get; set; }

        public string PasswordHash { get; set; }

        public int AccessFailedCount { get; set; }

        public IEnumerable<DemoUserRole> UserRoles { get; set; }

        public IEnumerable<DemoUserLogin> Logins { get; set; }

        public IEnumerable<DemoUserClaim> Claims { get; set; }

        public IEnumerable<DemoUserToken> Tokens { get; set; }

        #endregion
    }
}