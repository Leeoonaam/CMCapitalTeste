namespace CMCapital.Application.Dtos.Response
{
    public class BaseResponse
    {
        public required bool Status { get; set; }
        public string? Mensagem { get; set; }
        public object? Resultado { get; set; }
        public bool SemPermissao { get; set; }
        public List<string>? Erros { get; set; }
    }

    public class ListaTabelaDeDados
    {
        public ListaTabelaDeDados()
        {
            IndexChave = new IndexChave();
            Cabecalho = new Dictionary<string, string>();
            Colunas = new List<ColunaTabela>();
            Valores = new Valores();
        }

        public IndexChave IndexChave { get; set; }
        public Dictionary<string, string> Cabecalho { get; set; }
        public List<ColunaTabela> Colunas { get; set; }
        public Valores Valores { get; set; }
    }

    public class ColunaTabela
    {
        public string? Campo { get; set; }
        public string? Cabecalho { get; set; }
    }

    public class Valores
    {
        public Valores()
        {
            Valor = new List<Dictionary<string, dynamic>>();
        }
        public List<Dictionary<string, dynamic>> Valor { get; set; }
    }

    public class IndexChave
    {
        public IndexChave()
        {
            Index = new List<int>();
            Chave = new Dictionary<int, string>();
        }
        public List<int> Index { get; set; }
        public Dictionary<int, string> Chave { get; set; }
    }
}