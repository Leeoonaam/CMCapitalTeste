using CMCapital.Application.Dtos.Response;
using System.Dynamic;

namespace CMCapital.Application.Utils
{
    public static class TabelaUtils
    {
        public static TabelaDinamica ConstruirTabelaDinamica<T>(IEnumerable<T> listaDTO, Dictionary<string, string>? mapeamentoHeaders = null)
        {
            var tabelaDinamica = new TabelaDinamica();
            tabelaDinamica.Colunas = new List<Coluna>();
            tabelaDinamica.Valores = new List<dynamic>();

            var propriedadesDTO = typeof(T).GetProperties();
            foreach (var propriedade in propriedadesDTO)
            {
                var nomePropriedade = propriedade.Name;
                var header = mapeamentoHeaders != null && mapeamentoHeaders.ContainsKey(nomePropriedade)
                    ? mapeamentoHeaders[nomePropriedade]
                    : nomePropriedade;

                tabelaDinamica.Colunas.Add(new Coluna
                {
                    Field = nomePropriedade,
                    Header = header,
                    Oculto = false
                });
            }

            foreach (var dto in listaDTO)
            {
                var objetoDinamico = new ExpandoObject() as IDictionary<string, object>;
                foreach (var propriedade in propriedadesDTO)
                {
                    var nomePropriedade = propriedade.Name;
                    var coluna = tabelaDinamica.Colunas.FirstOrDefault(c => c.Field == nomePropriedade);

                    if (coluna != null && !coluna.Oculto)
                    {
                        objetoDinamico[nomePropriedade] = propriedade.GetValue(dto)!;
                    }
                }
                tabelaDinamica.Valores.Add(objetoDinamico);
            }

            return tabelaDinamica;
        }
    }
}