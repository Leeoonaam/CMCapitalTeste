namespace CMCapital.Application.Dtos.Response
{
    public class TabelaDinamica
    {
        public int IndexDeletado { get; set; } = 0;
        public List<Coluna>? Colunas { get; set; }
        public List<dynamic>? Valores { get; set; }
    }

    public class Coluna
    {
        public string? Field { get; set; }
        public string? Header { get; set; }
        public bool Oculto { get; set; }
        public bool EhEditavel { get; set; }
        public bool EhTelefone { get; set; }
        public bool EhSecreto { get; set; }
        public bool EhCor { get; set; }
        public bool EhDate { get; set; }
        public bool EhDateTime { get; set; }
        public bool EhMoeda { get; set; }
        public bool EhLink { get; set; }
        public bool EhBoleano { get; set; }
        public bool EhBoleanoBloqueado { get; set; }
        public bool EhCnpjCpf { get; set; }
        public int valorAlterado { get; set; } = 0;
        public bool ehBotao { get; set; }
        public bool ehRg { get; set; }
        public bool EhPascalCase { get; set; }
    }
}
