using System;
using System.Configuration;
using System.Threading.Tasks;
using Eventus.Cleanup;
using Eventus.DocumentDb;
using Eventus.DocumentDb.Config;
using Eventus.Logging;
using Eventus.Storage;
using Microsoft.Azure.Documents.Client;

namespace Eventus.Samples.Infrastructure.Factories
{
    public abstract partial class StorageProviderFactory
    {
        private class DocumentDbProviderFactory : StorageProviderFactory
        {
            private static DocumentClient _client;

            public DocumentDbProviderFactory(int value, string displayName) : base(value, displayName)
            {
            }

            public override Task<ITeardown> CreateTeardownAsync()
            {
                return Task.FromResult<ITeardown>(new DocumentDbTeardown(Client, DatabaseId));
            }

            public override Task<IEventStorageProvider> CreateEventStorageProviderAsync()
            {
                var provider = new DocumentDbStorageProvider(Client, DatabaseId);
                return Task.FromResult<IEventStorageProvider>(new EventStorageProviderLoggingDecorator(provider));
            }

            public override Task<ISnapshotStorageProvider> CreateSnapshotStorageProviderAsync()
            {
                var provider = new DocumentDbSnapShotProvider(Client, DatabaseId, 3);
                return Task.FromResult<ISnapshotStorageProvider>(new SnapshotProviderLoggingDecorator(provider));
            }

            public override Task InitAsync()
            {
                var init = new DocumentDbInitialiser(Client, new DocumentDbConfig(DatabaseId, 400, 400));
                return init.InitAsync();
            }

            private static DocumentClient Client => _client ?? (_client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["DocumentDb.Endpoint"] ?? "https://localhost:8081/"), ConfigurationManager.AppSettings["DocumentDb.AuthKey"] ?? "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", new ConnectionPolicy { EnableEndpointDiscovery = false }));

            private static readonly string DatabaseId = ConfigurationManager.AppSettings["DocumentDb.DatabaseId"] ?? "Eventus";
        }
    }
}