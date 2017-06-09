using System.Configuration;
using System.Threading.Tasks;

namespace Eventus.Samples.Infrastructure.Factories
{
    public class StorageProviderInitialiser
    {
        public static Task InitAsync(string provider = "")
        {
            var providerName = string.IsNullOrWhiteSpace(provider) ? ConfigurationManager.AppSettings[Constants.Provider].ToLowerInvariant() : provider;
            var p = StorageProviderFactory.FromString(providerName);
            return p.InitAsync();
        }
    }
}