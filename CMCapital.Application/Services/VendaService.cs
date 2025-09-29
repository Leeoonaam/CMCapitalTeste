using CMCapital.Application.Dtos.Adicionar;
using CMCapital.Application.Dtos.Alterar;
using CMCapital.Application.Dtos.Deletar;
using CMCapital.Application.Dtos.Response;
using CMCapital.Application.Interfaces;
using CMCapital.Application.Utils;
using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMCapital.Application.Services
{
    public class VendaService : BaseService, IBaseService, IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public VendaService(IVendaRepository vendaRepository,
            IClienteRepository clienteRepository,
            IProdutoRepository produtoRepository,
            SessaoUsuario sessaoUsuario, ILogger logger) : base(sessaoUsuario, logger)
        {
            _vendaRepository = vendaRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<BaseResponse> Incluir(AdicionarVendaRequest model)
        {
            try
            {
                var cliente = await _clienteRepository.BuscarPorId(model.ClienteId);
                if (cliente == null) 
                    return new BaseResponse() { Status = false, Mensagem = "Cliente não encontrado." };

                var produto = await _produtoRepository.BuscarUm(model.ProdutoId);
                if(produto == null)
                    return new BaseResponse() { Status = false, Mensagem = "Produto não encontrado." };

                if(model.Quantidade <= 0)
                    return new BaseResponse() { Status = false, Mensagem = "É necessário informar a quantidade da Venda." };

                if (model.Quantidade > produto.Quantidade)
                    return new BaseResponse() { Status = false, Mensagem = "Não é permitido realizar uma venda de quantidade maior do produto disponível. Quantidade Atual: " + produto.Quantidade };

                var valorTotal = (model.Quantidade * produto.Preco);
                if(cliente.SaldoDisponivel < valorTotal)
                    return new BaseResponse() { Status = false, Mensagem = "Não é permitido realizar uma venda se o saldo do cliente for menor que o valor total dos produtos a serem vendidos; " };

                var venda = new TblVendum
                {
                    ClienteId = model.ClienteId,
                    ProdutoId = model.ProdutoId,
                    Quantidade = model.Quantidade,
                    UsuarioIdInsert = _sessaoUsuario.GetId(),
                    DthInsert = DateTime.Now,
                };

                var ret = await _vendaRepository.Add(venda);
                if (ret == 0)
                    return new BaseResponse() { Status = false, Mensagem = "Erro ao realizar Venda." };

                cliente.SaldoDisponivel -= valorTotal;
                cliente.DthUpdate = DateTime.Now;
                cliente.UsuarioIdUpdate = _sessaoUsuario.GetId();

                var retCliente = await _clienteRepository.Update(cliente);
                if(retCliente == 0)
                    return new BaseResponse() { Status = false, Mensagem = "Erro ao atualizar Saldo Disponivel do Cliente." };

                produto.Quantidade -= model.Quantidade;
                produto.DthUpdate = DateTime.Now;
                produto.UsuarioIdUpdate = _sessaoUsuario.GetId();

                var retProduto = await _produtoRepository.Update(produto);
                if(retProduto == 0)
                    return new BaseResponse() { Status = false, Mensagem = "Erro ao atualizar Quantidade de Produto no Estoque." };

                return new BaseResponse() { Status = true, Mensagem = "Venda realizada com Sucesso." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao incluir Venda");
                return new BaseResponse() { Status = false, Mensagem = "Erro ao Incluir Venda." };
            }
        }
        public Task<BaseResponse> Listar()
        {
            throw new NotImplementedException();
        }
    }
}