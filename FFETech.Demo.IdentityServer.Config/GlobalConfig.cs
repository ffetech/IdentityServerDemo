namespace FFETech.Demo.IdentityServer.Config
{
    public static class GlobalConfig
    {
        public const string DefaultUserName = "test@test.com";
        public const string DefaultUserPasswordHash = "AQAAAAEAACcQAAAAEAq+nzVDczC+Y1WTMfFJ3qcWgcq3teVyuQSqx8bzl6gQ6FSFhzinrneZlgLRkMXepg=="; // Test123$ 

        public const int ProxyPort = 5000;
        public const string IdentityServerId = "auth";
        public const int IdentityServerPort = 5001;
        public const int IdentityServerPortForClient = ProxyPort; // switch here between ProxyPort and IdentityServerPort

        public const string RazorClientId = "razor";
        public const string RazorClientSecret = "razor-secret";
        public const int RazorPort = 5002;

        public const string BlazorClientId = "blazor";
        public const string BlazorClientSecret = "blazor-secret";
        public const int BlazorPort = 5003;

        public const string ApiClientId = "api";
        public const string ApiClientSecret = "api-secret";
        public const int ApiPort = 5004;
    }
}
