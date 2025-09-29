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
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _ProdutoService;
        public ProdutoController(IProdutoService controller)
        {
            _ProdutoService = controller;
        }

        [HttpGet("Listar")]
        [Authorize(Roles = RolesAuthorize.UsuarioRole)]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _ProdutoService.Listar());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = "Falha na autenticação do sistema.", Resultado = ex.Message });
            }
        }

        [HttpPost("Incluir")]
        [Authorize(Roles = RolesAuthorize.UsuarioRole)]
        public async Task<IActionResult> Incluir([FromBody] AdicionarProdutoRequest model)
        {
            try
            {
                return Ok(await _ProdutoService.Incluir(model));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = "Falha na autenticação do sistema.", Resultado = ex.Message });
            }
        }

        [HttpPost("Alterar")]
        [Authorize(Roles = RolesAuthorize.UsuarioRole)]
        public async Task<IActionResult> Alterar([FromBody] AlterarProdutoRequest model)
        {
            try
            {
                return Ok(await _ProdutoService.Incluir(model));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = "Falha na autenticação do sistema.", Resultado = ex.Message });
            }
        }

        [HttpPost("Deletar")]
        [Authorize(Roles = RolesAuthorize.UsuarioRole)]
        public async Task<IActionResult> Deletar([FromForm] int id)
        {
            try
            {
                return Ok(await _ProdutoService.Deletar(id));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = "Falha na autenticação do sistema.", Resultado = ex.Message });
            }
        }
    }
}