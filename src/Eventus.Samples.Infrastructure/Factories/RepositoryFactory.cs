using System.Configuration;
using System.Threading.Tasks;
using Eventus.Storage;

namespace Eventus.Samples.Infrastructure.Factories
{
    public class RepositoryFactory
    {
        public static async Task<IRepository> CreateAsync(string provider = "")
        {
            var providerName = string.IsNullOrWhiteSpace(provider) ? ConfigurationManager.AppSettings[Constants.Provider].ToLowerInvariant() : provider;
            var p = StorageProviderFactory.FromString(providerName);
            var repo = await p.CreateRepositoryAsync().ConfigureAwait(false);
            return repo;
        }
    }
}
