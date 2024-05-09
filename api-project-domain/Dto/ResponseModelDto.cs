namespace api_project_domain.Dto
{
    public class ResponseModelDto<T> 
    {
        public T Data { get; set; } 
        private List<string> Errors { get; set; } = [];
        public Paginacao Paginacao { get; set; }
        public bool Sucess {  get; set; } = true;

        public void AdicionarErro(string newError)
        {
            Sucess = false;
            Errors.Add(newError);
        }
        
        public void AdicionarErros(List<string> newErrors)
        {
            Sucess = false;
            Errors = newErrors;
        }
    }

    public class Paginacao
    {
        public Paginacao(int? pagina, int quantidadeItens, int total)
        {
            Pagina = pagina;
            QuantidadeDePaginas = total == 0 ? 0 : (int)Math.Ceiling(total / (decimal)quantidadeItens);
            Total = total;
            QuantidadeItens = quantidadeItens;
        }
           
        public int? Pagina { get; set; }
        public int? QuantidadeDePaginas { get; set; }
        public int? QuantidadeItens { get; set; }
        public int? Total { get; set; }
    }
}
