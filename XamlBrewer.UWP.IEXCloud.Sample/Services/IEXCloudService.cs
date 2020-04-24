using VSLee.IEXSharp;

namespace XamlBrewer.UWP.IEXCloud.Sample.Services
{
    public static class IEXCloudService
    {
        public static IEXCloudClient GetClient()
        {
            var settings = new Settings();

            if (settings.UseSandBox)
            {
                return GetSandBoxClient();
            }

            return GetProductionClient();
        }

        public static IEXCloudClient GetSandBoxClient()
        {
            var settings = new Settings();

            return new IEXCloudClient(
                publishableToken: settings.PublishableSandBoxKey,
                secretToken: settings.SecretSandBoxKey,
                signRequest: false,
                useSandBox: true);
        }

        public static IEXCloudClient GetProductionClient()
        {
            var settings = new Settings();

            return new IEXCloudClient(
                publishableToken: settings.PublishableKey,
                secretToken: settings.SecretKey,
                signRequest: false,
                useSandBox: false);
        }
    }
}
