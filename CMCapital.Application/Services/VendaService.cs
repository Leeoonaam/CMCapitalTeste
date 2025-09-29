using CMCapital.Application.Dtos.Adicionar;
using CMCapital.Application.Dtos.Alterar;
using CMCapital.Application.Dtos.Deletar;
using CMCapital.Application.Dtos.Response;
using CMCapital.Application.Interfaces;
using CMCapital.Application.Utils;
using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace CMCapital.Application.Services
{
    public class VendaService : BaseService, IBaseService, IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IClienteRepository _clienteRepository;


        public VendaService(IVendaRepository vendaRepository,
            IClienteRepository clienteRepository,
            SessaoUsuario sessaoUsuario) : base(sessaoUsuario)
        {
            _vendaRepository = vendaRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<BaseResponse> Incluir(AdicionarVendaRequest model)
        {
            try
            {
                var cliente = await _clienteRepository.BuscarPorId(model.ClienteId);
                if (cliente == null) 
                    return new BaseResponse() { Status = false, Mensagem = "Cliente não encontrado." };

                //var produto = await 

                var venda = new TblVendum
                {
                    ClienteId = cliente.ClienteId,
                    Quantidade = model.Quantidade,
                    UsuarioIdInsert = _sessaoUsuario.GetId(),
                    DthInsert = DateTime.Now,
                };

                var ret = await _vendaRepository.Add(venda);
                if (ret == 0)
                    return new BaseResponse() { Status = false, Mensagem = "Erro ao realizar Venda." };
                

                return new BaseResponse() { Status = true, Mensagem = "Venda realizada com Sucesso." };
                
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = false, Mensagem = ex.Message, Resultado = ex.StackTrace };
            }
        }

        public Task<BaseResponse> Listar()
        {
            throw new NotImplementedException();
        }
    }
}