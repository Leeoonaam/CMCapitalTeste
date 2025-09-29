using CMCapital.Application.Utils;

namespace CMCapital.Application.Services
{
    public class BaseService
    {
        public readonly SessaoUsuario _sessaoUsuario;

        public BaseService(SessaoUsuario sessaoUsuario)
        {
            _sessaoUsuario = sessaoUsuario;
        }
    }
}