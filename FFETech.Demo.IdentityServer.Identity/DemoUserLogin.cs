namespace FFETech.Demo.IdentityServer.Identity
{
    public class DemoUserLogin
    {
        #region Properties

        public DemoUser User { get; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public string ProviderDisplayName { get; set; }

        #endregion
    }
}