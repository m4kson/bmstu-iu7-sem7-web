namespace ProdMonitor.Application.Services.Configurations
{
    public class AuthenticationServiceConfiguration
    {
        public static readonly string ConfigurationSectionName = "BusinessLogic";
        public required int MinPasswordLength { get; init; }
    }
}
