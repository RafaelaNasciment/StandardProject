using api_project_domain.Entity;
using api_project_infrastructure.Configuration;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static api_project_repository.Repository.FiltrosRepository;

namespace api_project_repository.Repository
{
    public class RepositorioBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        readonly CosmosClient _client;

        readonly string _dataBase = "LukreyDB";
        private readonly ConfiguracoesGlobais _configuracoesGlobais;
        public RepositorioBase(
            ConfiguracoesGlobais configuracoesGlobais)
        {
            _configuracoesGlobais = configuracoesGlobais;
            _client = new(connectionString: _configuracoesGlobais.ConnectionString);
        }

        public async Task AddEntityAsync(TEntity entity, string partitionKey)
        {
            //TODO: validar existencia da base e container de forma unica
            //não precisar validar em todas requisições
            //await CriarContainer(container, partintionKeyName);

            entity.IdContainer = typeof(TEntity).Name + "|" + entity.IdRegistro;
            entity.Discriminator = typeof(TEntity).Name + "|" + entity.IdRegistro;
            await _client.GetContainer(_dataBase, typeof(TEntity).Name).CreateItemAsync(entity, new PartitionKey(partitionKey.ToString()));
        }

        public async Task UpdateEntityAsync(TEntity entity, string partitionKey)
        {
            entity.IdContainer = typeof(TEntity).Name + "|" + entity.IdRegistro;
            entity.Discriminator = typeof(TEntity).Name + "|" + entity.IdRegistro;
            await _client.GetContainer(_dataBase, typeof(TEntity).Name).ReplaceItemAsync(entity, typeof(TEntity).Name + "|" + entity.IdRegistro, new PartitionKey(partitionKey.ToString()));
        }

        public async Task<IList<TEntity>> EntitiesAsync(string query, Dictionary<string, object> parameters)
        {
            QueryDefinition queryDefinition = new(query);
            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    queryDefinition = queryDefinition.WithParameter(parameter.Key, parameter.Value);
                }
            }

            FeedIterator<TEntity> queryResultSetIterator = _client.GetContainer(_dataBase, typeof(TEntity).Name).GetItemQueryIterator<TEntity>(queryDefinition);
            List<TEntity> entities = new();
            while (queryResultSetIterator.HasMoreResults)
            {
                foreach (TEntity item in await queryResultSetIterator.ReadNextAsync())
                {
                    entities.Add(item);
                }
            }

            return entities;
        }

        public async Task CriarContainer(string container, string partitionKey)
        {
            Database database = await _client.CreateDatabaseIfNotExistsAsync(
            id: _dataBase
            );

            await database.CreateContainerIfNotExistsAsync(
            id: container,
            partitionKeyPath: $"/{partitionKey}"
            );
        }

        public async Task<int> QuantidadeItensAsync(string query, Dictionary<string, object> parameters)
        {
            QueryDefinition queryDefinition = new QueryDefinition(query);
            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    queryDefinition = queryDefinition.WithParameter(parameter.Key, parameter.Value);
                }
            }

            FeedResponse<int> feedResponse = await _client.GetContainer(_dataBase, typeof(TEntity).Name).GetItemQueryIterator<int>(query).ReadNextAsync();
            int result = 0;
            if (feedResponse != null && feedResponse != null && feedResponse.Count > 0)
                result = feedResponse.FirstOrDefault();

            return result;
        }

        public string ConfigurarCamposRetorno(string[]? camposRetorno, string entidade)
        {
            string retorno = string.Empty;
            if (camposRetorno != null)
            {
                int cont = 0;
                foreach (var campo in camposRetorno)
                {
                    retorno += $"{entidade}.{campo}";
                    retorno += cont < camposRetorno.Length - 1 ? ", " : " ";
                    cont++;
                }

                return retorno;
            }

            return "*";
        }

        protected async Task DeleteEntity(string idContainer, string partitionKey)
        {
            await _client.GetContainer(_dataBase, typeof(TEntity).Name).DeleteItemAsync<TEntity>(id: idContainer, partitionKey: new PartitionKey(partitionKey));
        }
    }
}
