using CMCapital.Application.Dtos.Adicionar;
using CMCapital.Application.Dtos.Deletar;
using CMCapital.Application.Dtos.Alterar;
using CMCapital.Application.Dtos.Enums;
using CMCapital.Application.Dtos.Response;
using CMCapital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMCapital.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : Controller
    {
        private readonly IVendaService _vendaService;
        public VendaController(IVendaService controller)
        {
            _vendaService = controller;
        }

        [HttpGet("Listar")]
        [Authorize(Roles = RolesAuthorize.UsuarioRole)]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _vendaService.Listar());

            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = ex.Message });
            }
        }

        [HttpPost("Incluir")]
        [Authorize(Roles = RolesAuthorize.UsuarioRole)]
        public async Task<IActionResult> Incluir([FromBody] AdicionarVendaRequest model)
        {
            try
            {
                return Ok(await _vendaService.Incluir(model));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = ex.Message });
            }
        }

        [HttpPost("Alterar")]
        [Authorize(Roles = RolesAuthorize.UsuarioRole)]
        public async Task<IActionResult> Alterar([FromBody] AlterarClienteRequest model)
        {
            try
            {
                return Ok(await _vendaService.Alterar(model));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = ex.Message });
            }
        }

        [HttpPost("Deletar")]
        [Authorize(Roles = RolesAuthorize.UsuarioRole)]
        public async Task<IActionResult> Deletar([FromBody] DeletarClienteRequest model)
        {
            try
            {
                return Ok(await _vendaService.Deletar(model));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = ex.Message });
            }
        }

    }
}