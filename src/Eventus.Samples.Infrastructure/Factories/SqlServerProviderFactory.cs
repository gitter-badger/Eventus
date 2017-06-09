﻿using System.Configuration;
using System.Threading.Tasks;
using Eventus.Cleanup;
using Eventus.SqlServer;
using Eventus.SqlServer.Config;
using Eventus.Storage;

namespace Eventus.Samples.Infrastructure.Factories
{
    public abstract partial class StorageProviderFactory
    {
        private class SqlServerProviderFactory : StorageProviderFactory
        {
            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Eventus"] == null ? "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=Eventus;Integrated Security=True" : ConfigurationManager.ConnectionStrings["Eventus"].ToString();

            public SqlServerProviderFactory(int value, string displayName) : base(value, displayName)
            {
            }

            public override Task<ITeardown> CreateTeardownAsync()
            {
                return Task.FromResult<ITeardown>(new SqlServerTeardown(_connectionString));
            }

            public override Task<IEventStorageProvider> CreateEventStorageProviderAsync()
            {
                return Task.FromResult<IEventStorageProvider>(new SqlServerEventStorageProvider(_connectionString));
            }

            public override Task<ISnapshotStorageProvider> CreateSnapshotStorageProviderAsync()
            {
                return Task.FromResult<ISnapshotStorageProvider>(new SqlServerSnapshotStorageProvider(
                    _connectionString, 3));
            }

            public override Task InitAsync()
            {
                var init = new SqlProviderInitialiser(new SqlServerConfig(_connectionString));
                return init.InitAsync();
            }
        }
    }
}