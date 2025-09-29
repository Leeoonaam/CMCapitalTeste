using CMCapital.Application.Dtos.Request;
using CMCapital.Application.Dtos.Response;

namespace CMCapital.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<BaseResponse> Listar();
        Task<BaseResponse> Incluir(AdicionarProdutoRequest model);
        Task<BaseResponse> Alterar(AlterarProdutoRequest model);
        Task<BaseResponse> Deletar(int produtoId);
    }
}