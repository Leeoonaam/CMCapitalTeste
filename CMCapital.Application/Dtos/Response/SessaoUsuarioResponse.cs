using CMCapital.Application.Dtos.Response;

namespace CMCapital.Application.Dtos.Response
{
    public class SessaoUsuarioResponse : UsuarioResponse
    {
        public int UsuarioId { get; set; }
        public string? CPF { get; set; }
        public DateTime? VencimentoSessao { get; set; }
    }
}