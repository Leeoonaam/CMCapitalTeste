using CMCapital.Application.Utils;
using Microsoft.Extensions.Logging;

namespace CMCapital.Application.Services
{
    public class BaseService
    {
        public readonly SessaoUsuario _sessaoUsuario;
        public readonly ILogger _logger;

        public BaseService(SessaoUsuario sessaoUsuario, ILogger logger)
        {
            _sessaoUsuario = sessaoUsuario;
            _logger = logger;
        }

    }
}