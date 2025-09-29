using CMCapital.Application.Dtos.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CMCapital.Application.Utils
{
    public class SessaoUsuario
    {
        private readonly IHttpContextAccessor _accessor;

        public SessaoUsuario(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string UsuarioId => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Sid)?.Value!;
        public string Cpf => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.SerialNumber)?.Value!;
        public DateTime VencimentoSessao => Convert.ToDateTime(GetClaimsIdentity().FirstOrDefault(a => a.Type == "VencimentoSessao")?.Value);
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor?.HttpContext?.User.Claims!;
        }

        public HttpRequest GetUserRequest()
        {
            return _accessor?.HttpContext?.Request!;
        }

        public string GetToken()
        {
            string userAuth = _accessor?.HttpContext?.Request.Headers["authorization"].ToString()!;
            userAuth = userAuth.Replace("Bearer ", "");

            return userAuth;
        }

        public ConnectionInfo GetUserConnection()
        {
            return _accessor?.HttpContext?.Connection!;
        }

        public int GetId()
        {
            return int.Parse(UsuarioId ?? "0");
        }
    }
}