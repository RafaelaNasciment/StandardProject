using api_project_domain.Entity;

namespace api_project_repository.Interfaces
{
    public interface IProductServiceRepository
    {
        Task CadastrarAsync(Produto entity);

        Task EditarAsync(Produto entity);

        Task<IList<Produto>> ListarAsync(Produto entity);

        Task DeleteAsync(Produto entity);
    }
}
