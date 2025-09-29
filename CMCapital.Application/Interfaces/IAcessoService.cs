using CMCapital.Application.Dtos.Request;
using CMCapital.Application.Dtos.Response;

namespace CMCapital.Application.Interfaces
{
    public interface IAcessoService
    {
        Task<BaseResponse> Login(LoginRequest model);
    }
}