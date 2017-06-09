using System.Configuration;
using System.Threading.Tasks;
using Eventus.Storage;

namespace Eventus.Samples.Infrastructure.Factories
{
    public class SnapshotStorageProviderFactory
    {
        public static async Task<ISnapshotStorageProvider> CreateAsync(string provider = "")
        {
            var providerName = string.IsNullOrWhiteSpace(provider) ? ConfigurationManager.AppSettings[Constants.Provider].ToLowerInvariant() : provider;
            var p = StorageProviderFactory.FromString(providerName);
            var repo = await p.CreateSnapshotStorageProviderAsync().ConfigureAwait(false);
            return repo;
        }
    }
}