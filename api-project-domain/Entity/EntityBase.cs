using Newtonsoft.Json;

namespace api_project_domain.Entity
{
    public class EntityBase
    {
        protected EntityBase(string idUsuarioCriacaoEntidade)
        {
            IdRegistro = Guid.NewGuid().ToString();
            IdRegistro2 = Guid.NewGuid().ToString();
            DataCriacao = DateTime.UtcNow.AddHours(-3);
            DataAtualizacao = DataCriacao;
            IdUsuarioCriacao = idUsuarioCriacaoEntidade.Trim();
            IdUsuarioAtualizacao = IdUsuarioCriacao;
            Ativo = true;
        }

        [JsonProperty(PropertyName = "id")]
        public string IdContainer { get; set; }

        public string Discriminator { get; set; }
        public string IdRegistro { get; set; }

        public string IdRegistro2 { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public string IdUsuarioCriacao { get; set; }

        public string IdUsuarioAtualizacao { get; set; }

        public bool Ativo { get; set; }
    }
}
