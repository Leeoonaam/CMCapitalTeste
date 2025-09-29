using CMCapital.Application.Dtos.Enums;
using CMCapital.Application.Dtos.Request;
using CMCapital.Application.Dtos.Response;
using CMCapital.Application.Interfaces;
using CMCapital.Application.Utils;
using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace CMCapital.Application.Services
{
    public class ClienteService : BaseService, IBaseService, IClienteService
    {
        private readonly IClienteRepository _clienteoRepository;

        public ClienteService(IClienteRepository clienteoRepository,
            SessaoUsuario sessaoUsuario) : base(sessaoUsuario)
        {
            _clienteoRepository = clienteoRepository;
        }

        public async Task<BaseResponse> Incluir(AdicionarClienteRequest model)
        {
            try
            {
                var existe = await _clienteoRepository.BuscarPorNome(model.Nome);
                if (existe != null)
                {
                    return new BaseResponse() { Status = false, Mensagem = "Já existe um cliente com esse nome." };
                
                }
                return new BaseResponse() { Status = false, Mensagem = "Já existe um cliente com esse nome." };
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = false, Mensagem = ex.Message, Resultado = ex.StackTrace };
            }
        }
    }
}