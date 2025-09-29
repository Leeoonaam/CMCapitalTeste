using CMCapital.Application.Dtos.Request;
using CMCapital.Application.Dtos.Response;

namespace CMCapital.Application.Interfaces
{
    public interface IClienteService
    {
        Task<BaseResponse> Incluir(AdicionarClienteRequest model);
    }
}