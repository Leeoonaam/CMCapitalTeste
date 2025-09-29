using CMCapital.Application.Dtos.Adicionar;
using CMCapital.Application.Dtos.Alterar;
using CMCapital.Application.Dtos.Deletar;
using CMCapital.Application.Dtos.Response;
using CMCapital.Application.Interfaces;
using CMCapital.Application.Utils;
using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CMCapital.Application.Services
{
    public class ClienteService : BaseService, IBaseService, IClienteService
    {
        private readonly IClienteRepository _clienteoRepository;

        public ClienteService(IClienteRepository clienteoRepository,
            SessaoUsuario sessaoUsuario, ILogger logger) : base(sessaoUsuario, logger)
        {
            _clienteoRepository = clienteoRepository;
        }

        public async Task<BaseResponse> Listar()
        {
            try
            {
                var clientes = await _clienteoRepository.BuscarTodos();

                var resultado = clientes?.Select(c => new ClienteResponse
                {
                    ClienteId = c.ClienteId,
                    Nome = c.Nome,
                    SaldoDisponivel = c.SaldoDisponivel,
                    DataCadastro = c.DthInsert,
                }).ToList();

                var mapeamentoHeaders = new Dictionary<string, string>
                {
                    { "ClienteId", "ID" },
                    { "Nome", "Nome" },
                    { "SaldoDisponivel", "Saldo Disponível" },
                    { "DataCadastro", "Data de Cadastro" },
                };

                var tabelaDinamica = TabelaUtils.ConstruirTabelaDinamica(resultado!, mapeamentoHeaders);

                tabelaDinamica.Colunas!.First(c => c.Field == "ClienteId").Oculto = true;
                tabelaDinamica.Colunas!.First(c => c.Field == "DataCadastro").EhDateTime = true;
                tabelaDinamica.Colunas!.First(c => c.Field == "DataCadastro").EhMoeda = true;

                return new BaseResponse() { Status = true, Resultado = tabelaDinamica };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao Listar Clientes.", ex.StackTrace);
                return new BaseResponse { Status = false, Mensagem = "Erro ao Listar Clientes." };
            }
        }
        public async Task<BaseResponse> Incluir(AdicionarClienteRequest model)
        {
            try
            {
                if (model.SaldoDisponivel < 0)
                    return new BaseResponse() { Status = false, Mensagem = "Não é permitido cadastro de clientes com saldo menor que zero." };

                var existe = await _clienteoRepository.BuscarPorNome(model.Nome);
                if (existe != null)
                {
                    if (existe.DthDelete == null)
                        return new BaseResponse() { Status = false, Mensagem = "Já existe um cliente cadastrado com esse nome!" };

                    existe.Nome = model.Nome;
                    existe.DthDelete = null;
                    existe.UsuarioIdDelete = null;
                    existe.DthUpdate = DateTime.Now;
                    existe.UsuarioIdUpdate = _sessaoUsuario.GetId();

                    var ret = await _clienteoRepository.Update(existe);
                    if (ret == 0)
                        return new BaseResponse() { Status = false, Mensagem = "Erro ao reativar Cliente." };

                    return new BaseResponse() { Status = true, Mensagem = "Cliente reativado com Sucesso.", Resultado = existe };
                }
                else
                {
                    var novoCliente = new TblCliente
                    {
                        Nome = model.Nome,
                        SaldoDisponivel = model.SaldoDisponivel,
                        UsuarioIdInsert = _sessaoUsuario.GetId(),
                        DthInsert = DateTime.Now,
                    };

                    var retNovoCliente = await _clienteoRepository.Add(novoCliente);
                    if (retNovoCliente == 0)
                        return new BaseResponse() { Status = false, Mensagem = "Erro ao incluir novo Cliente." };

                    return new BaseResponse() { Status = true, Mensagem = "Cliente incluído com Sucesso." };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao incluir Cliente.", ex.StackTrace);
                return new BaseResponse() { Status = false, Mensagem = "Erro ao incluir Cliente." };
            }
        }
        public async Task<BaseResponse> Alterar(AlterarClienteRequest model)
        {
            try
            {
                var cliente = await _clienteoRepository.BuscarPorId(model.ClienteId);
                if (cliente == null)
                {
                    return new BaseResponse { Status = false, Mensagem = "Cliente não encontrado!" };
                }

                cliente.Nome = model.Nome;
                cliente.SaldoDisponivel = model.SaldoDisponivel;
                cliente.DthUpdate = DateTime.Now;
                cliente.UsuarioIdUpdate = _sessaoUsuario.GetId();

                var ret = await _clienteoRepository.Update(cliente);
                if (ret == 0)
                {
                    return new BaseResponse { Status = false, Mensagem = "Erro ao alterar Cliente." };
                }

                return new BaseResponse { Status = true, Mensagem = "Cliente alterado com Sucesso.", Resultado = cliente };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Status = false, Mensagem = ex.Message, Resultado = ex.StackTrace };
            }
        }
        public async Task<BaseResponse> Deletar(DeletarClienteRequest model)
        {
            try
            {
                var cliente = await _clienteoRepository.BuscarPorId(model.ClienteId);
                if (cliente == null)
                {
                    return new BaseResponse { Status = false, Mensagem = "Cliente não encontrado!" };
                }

                cliente.UsuarioIdUpdate = _sessaoUsuario.GetId();
                cliente.DthUpdate = DateTime.Now;
                cliente.UsuarioIdDelete = _sessaoUsuario.GetId();
                cliente.DthDelete = DateTime.Now;

                var ret = await _clienteoRepository.Update(cliente);
                if (ret == 0)
                {
                    return new BaseResponse { Status = false, Mensagem = "Erro ao deletar Cliente." };
                }

                return new BaseResponse { Status = true, Mensagem = "Cliente deletado com Sucesso." };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Status = false, Mensagem = ex.Message, Resultado = ex.StackTrace };
            }
        }
    }
}