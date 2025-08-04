namespace Blog.Api
{
    public static class Constants
    {
        public static class ConfigurationKeys
        {
            public const string JwtSettingsSection = "JwtSettings";
            public const string DefaultConnectionString = "DefaultConnection";
        }

        public static class NotifyMethods
        {
            public const string ReceivePost = "ReceivePostNotification";
        }
    }
}
