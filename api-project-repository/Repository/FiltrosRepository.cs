namespace api_project_repository.Repository
{
    public class FiltrosRepository
    {
        public enum TipoFiltro
        {
            WHERE,
            AND,
            OR
        }
        public static string ConfigurarFiltro(string entidade, object campoParaFiltrar, object value, TipoFiltro tipoFiltro)
        {
            return $" {Enum.GetName(tipoFiltro)} {entidade}.{campoParaFiltrar} = '{value}' ";
        }

        public static string ConfigurarFiltroBoolean(string entidade, object campoParaFiltrar, object value, TipoFiltro tipoFiltro)
        {
            return $" {Enum.GetName(tipoFiltro)} {entidade}.{campoParaFiltrar} = {value?.ToString()?.ToLower()} ";
        }

        public static string ConfigurarPaginacao(int? paginaAtual, int? totalItensRetorno)
        {
            if (paginaAtual != null && totalItensRetorno != null)
            {
                var skip = (paginaAtual <= 0 ? 0 : paginaAtual - 1) * totalItensRetorno;
                return $" OFFSET {skip} LIMIT {totalItensRetorno} ";
            }

            return "";
        }

        public static string ConfigurarTotalItensRetorno(string entidade, string filtro, string join)
        {
            return $" SELECT VALUE COUNT(1) FROM {entidade} {join} {filtro}";
        }

        public static string Ordenar(string entidade, string campoParaOrdenar, bool asc)
        {
            string ordenacao = asc == true ? " ASC " : " DESC ";
            return $" ORDER BY {entidade}.{campoParaOrdenar} {ordenacao}";
        }

        public static string FiltrarPorListaDeIds(string entidade, List<string> ids, string campoParaFiltrar, TipoFiltro tipoFiltro)
        {
            string filtro = string.Empty;
            if (ids?.Count > 0)
            {
                int count = 0;
                foreach (var id in ids)
                {
                    count++;
                    filtro += string.IsNullOrWhiteSpace(filtro) ? $" {Enum.GetName(tipoFiltro)} ( " : " OR ";
                    filtro += @$"
                                {entidade}.{campoParaFiltrar} = '{id}'";
                }

                filtro += " ) ";
            }
            return filtro;
        }

        public static string ConfigurarFiltroPalavraChave(string entidade, object campoParaFiltrar, object? value, TipoFiltro tipoFiltro)
        {
            if (value == null)
                return string.Empty;

            return $" {Enum.GetName(tipoFiltro)} CONTAINS(UPPER({entidade}.{campoParaFiltrar}),'{value?.ToString()?.ToUpper()}') ";
        }

        public static TipoFiltro ValidandoTipoFiltro(string? filtro)
        {
            return string.IsNullOrWhiteSpace(filtro) ? TipoFiltro.WHERE : TipoFiltro.AND;
        }
    }
}
