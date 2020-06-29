namespace FFETech.Demo.IdentityServer.Identity
{
    public class DemoUserToken
    {
        #region Properties

        public DemoUser User { get; }

        public string LoginProvider { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        #endregion
    }
}