using CMCapital.Application.Dtos.Enums;
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
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService controller)
        {
            _clienteService = controller;
        }

        [HttpPost("Incluir")]
        [Authorize(Roles = RolesAuthorize.UsuarioRole)]
        public async Task<IActionResult> Incluir([FromBody] AdicionarClienteRequest model)
        {
            try
            {
                return Ok(await _clienteService.Incluir(model));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = "Falha na autenticação do sistema.", Resultado = ex.Message });
            }
        }
    }
}