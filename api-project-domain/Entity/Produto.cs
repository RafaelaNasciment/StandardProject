using api_project_domain.Enum;

namespace api_project_domain.Entity
{
    public class Produto : EntityBase
    {
        protected Produto(
            string idUsuarioCriacaoEntidade) : base(idUsuarioCriacaoEntidade)
        {
        }

        public int Quantity { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductStateEnum State { get; set; }
    }
}
