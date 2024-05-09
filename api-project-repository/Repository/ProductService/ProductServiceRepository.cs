using api_project_domain.Entity;
using api_project_infrastructure.Configuration;
using api_project_repository.Interfaces;

namespace api_project_repository.Repository.ProductService
{
    public class ProductServiceRepository : RepositorioBase<Produto>, IProductServiceRepository
    {
        private readonly string _alias = nameof(Produto);
        public ProductServiceRepository(
            ConfiguracoesGlobais configuracoesGlobais) : base(configuracoesGlobais)
        {
        }

        public async Task CadastrarAsync(Produto entity)
        {
            await AddEntityAsync(
                entity: entity,
                partitionKey: entity.IdRegistro);
        }

        public async Task DeleteAsync(Produto entity)
        {
            await UpdateEntityAsync(
                entity: entity,
                partitionKey: entity.IdRegistro);
        }

        public async Task EditarAsync(Produto entity)
        {
            await DeleteEntity(
                idContainer: entity.IdContainer, 
                partitionKey: entity.IdRegistro);
        }

        public async Task<IList<Produto>> ListarAsync(Produto entity)
        {
            string query =
                @$"
                    SELECT 
                        *
                    FROM 
                        {_alias} {_alias}
                ";

            IList<Produto> entities = await EntitiesAsync(
                query: query,
                parameters: null);

            return entities;
        }
    }
}
