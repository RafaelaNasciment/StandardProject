using Microsoft.Azure.Cosmos;

namespace api_project_infrastructure.CosmosClient
{
    public class CosmosClientFactory
    {
        readonly string _dataBase = "AppDaAcademia";
        readonly Microsoft.Azure.Cosmos.CosmosClient _client;

        public CosmosClientFactory(string connectionString)
        {
            _client = new(
                connectionString: connectionString);
        }

        public async Task CreateAsync(List<(string, string)> containers)
        {
            (string, string)[] array = ConfigureContainersDatabase(containers);

            (string container, string partitionKey)[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                (string, string) tuple = array2[i];
                await GetDatabase().CreateContainerIfNotExistsAsync(tuple.Item1, "/" + tuple.Item2);
            }
        }

        public Database GetDatabase()
        {
            return _client.GetDatabase(_dataBase);
        }

        public static (string container, string partitionKey)[] ConfigureContainersDatabase(List<(string, string)> containersContextDatabase)
        {
            var containersBase = new List<(string, string)>();

            if (containersContextDatabase != null && containersContextDatabase.Count > 0)
                containersBase.AddRange(containersContextDatabase);

            return containersBase.ToArray();
        }
    }
}
