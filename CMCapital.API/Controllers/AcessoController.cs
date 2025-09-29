using CMCapital.Application.Dtos.Request;
using CMCapital.Application.Dtos.Response;
using CMCapital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMCapital.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AcessoController : Controller
    {
        private readonly IAcessoService _acessoService;
        public AcessoController(IAcessoService controller)
        {
            _acessoService = controller;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            try
            {
                return Ok(await _acessoService.Login(model));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = "Falha na autenticação do sistema.", Resultado = ex.Message });
            }
        }
    }
}