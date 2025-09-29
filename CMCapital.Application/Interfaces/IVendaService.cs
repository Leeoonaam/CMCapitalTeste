using CMCapital.Application.Dtos.Adicionar;
using CMCapital.Application.Dtos.Alterar;
using CMCapital.Application.Dtos.Deletar;
using CMCapital.Application.Dtos.Response;

namespace CMCapital.Application.Interfaces
{
    public interface IVendaService
    {
        Task<BaseResponse> Listar();
        Task<BaseResponse> Incluir(AdicionarVendaRequest model);
    }
}