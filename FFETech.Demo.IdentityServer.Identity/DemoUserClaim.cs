namespace FFETech.Demo.IdentityServer.Identity
{
    public class DemoUserClaim
    {
        #region Properties

        public DemoUser User { get; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        #endregion
    }
}