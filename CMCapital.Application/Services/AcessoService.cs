using CMCapital.Application.Dtos.Request;
using CMCapital.Application.Dtos.Response;
using CMCapital.Application.Interfaces;
using CMCapital.Application.Utils;
using CMCapital.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CMCapital.Application.Services
{
    public class AcessoService : BaseService, IBaseService, IAcessoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly string _hash;

        public AcessoService(
            SessaoUsuario sessaoUsuario,
            IConfiguration configuracao,
            IUsuarioRepository usuarioRepository, ILogger logger) : base(sessaoUsuario, logger)
        {
            _hash = configuracao["Hash"]!;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<BaseResponse> Login(LoginRequest model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.cpf) || string.IsNullOrEmpty(model.senha))
                    return new BaseResponse { Status = false, Mensagem = $"Preencha todos os campos antes de tentar autenticar!" };

                model.cpf = StringUtils.PegarNumeros(model.cpf);
                model.senha = CriptografiaUtils.CriptografarSenha(model.senha);

                var usuario = await _usuarioRepository.ObterUsuarioPorCpfESenha(model.cpf, model.senha);

                if (usuario == null)
                    return new BaseResponse() { Status = false, Mensagem = "O Usuário que você tentou acessar não foi encontrado!" };

                if (usuario.DthDelete != null)
                    return new BaseResponse() { Status = false, Mensagem = "O Usuário que você tentou acessar não existe!" };
                
                if (usuario.Senha != model.senha)
                    return new BaseResponse() { Status = false, Mensagem = "A senha digitada está inválida!" };

                SessaoUsuarioResponse sessaoUsuario = new SessaoUsuarioResponse()
                {
                    UsuarioId = usuario.UsuarioId,
                    CPF = usuario.Cpf,
                    VencimentoSessao = DateTime.UtcNow.AddHours(1)
                };

                var token = CriptografiaUtils.GerarToken(sessaoUsuario, _hash);

                return new BaseResponse() { Status = true, Mensagem = token, Resultado = sessaoUsuario };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar Login.", ex.StackTrace);
                return new BaseResponse() { Status = false, Mensagem = "Erro ao realizar Login." };
            }
        }
    }
}