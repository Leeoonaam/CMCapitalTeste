using CMCapital.Application.Dtos.Enums;
using CMCapital.Application.Dtos.Request;
using CMCapital.Application.Dtos.Response;
using CMCapital.Application.Interfaces;
using CMCapital.Application.Utils;
using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CMCapital.Application.Services
{
    public class ProdutoService : BaseService, IBaseService, IProdutoService
    {
        private readonly IProdutoRepository _ProdutoRepository;

        public ProdutoService(IProdutoRepository ProdutooRepository,
            SessaoUsuario sessaoUsuario, ILogger logger) : base(sessaoUsuario, logger)
        {
            _ProdutoRepository = ProdutooRepository;
        }

        public async Task<BaseResponse> Listar()
        {
            try
            {
                var produtos = await _ProdutoRepository.BuscarTodos();

                if (produtos == null)
                    return new BaseResponse() { Status = false, Mensagem = "Nenhum produto encontrado!" };

                var resultado = produtos.Select(p => new ProdutoResponse()
                {
                    produtoId = p.ProdutoId,
                    nome = p.Nome,
                    preco = p.Preco,
                    quantidade = p.Quantidade
                }).ToList();

                return new BaseResponse() { Status = true, Resultado = resultado };
            }
            catch (Exception ex)
            {
                
                return new BaseResponse() { Status = false, Mensagem = "Ocorreu um erro ao listar produtos!" };
            }
        }

        public async Task<BaseResponse> Incluir(AdicionarProdutoRequest model)
        {
            try
            {
                var existeProduto = await _ProdutoRepository.BuscarUmPorNome(model.Nome);

                if (existeProduto != null)
                {
                    if (existeProduto.DthDelete == null)
                        return new BaseResponse() { Status = false, Mensagem = "Já existe um produto cadastrado com esse nome!" };

                    existeProduto.Quantidade = model.Quantidade;
                    existeProduto.Preco = model.Preco;
                    existeProduto.DthDelete = null;
                    existeProduto.UsuarioIdDelete = null;
                    existeProduto.UsuarioIdUpdate = _sessaoUsuario.GetId();
                    existeProduto.DthUpdate = DateTime.Now;

                    var retProduto = await _ProdutoRepository.Update(existeProduto);

                    if (retProduto > 0)
                    {
                        return new BaseResponse() { Status = true, Mensagem = "Produto cadastrado com sucesso!" };
                    }
                    else
                    {
                        return new BaseResponse() { Status = false, Mensagem = "Ocorreu um erro ao cadastrar o produto!" };
                    }
                }

                var novoProduto = new TblProduto()
                {
                    Nome = model.Nome,
                    Preco = model.Preco,
                    Quantidade = model.Quantidade,
                    UsuarioIdInsert = _sessaoUsuario.GetId(),
                    DthInsert = DateTime.Now
                };

                var ret = await _ProdutoRepository.Add(novoProduto);

                if (ret > 0)
                {
                    return new BaseResponse() { Status = true, Mensagem = "Produto cadastrado com sucesso!" };
                }
                else
                {
                    return new BaseResponse() { Status = false, Mensagem = "Ocorreu um erro ao cadastrar o produto!" };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = false, Mensagem = "Ocorreu um erro ao incluir produtos!" };
            }
        }

        public async Task<BaseResponse> Alterar(AlterarProdutoRequest model)
        {
            try
            {
                var existeProduto = await _ProdutoRepository.BuscarUmPorNome(model.Nome);

                if (existeProduto == null)
                    return new BaseResponse() { Status = false, Mensagem = "Produto não encontrado!" };

                existeProduto.Quantidade = model.Quantidade;
                existeProduto.Preco = model.Preco;
                existeProduto.DthDelete = null;
                existeProduto.UsuarioIdDelete = null;
                existeProduto.UsuarioIdUpdate = _sessaoUsuario.GetId();
                existeProduto.DthUpdate = DateTime.Now;

                var retProduto = await _ProdutoRepository.Update(existeProduto);

                if (retProduto > 0)
                {
                    return new BaseResponse() { Status = true, Mensagem = "Produto alterado com sucesso!" };
                }
                else
                {
                    return new BaseResponse() { Status = false, Mensagem = "Ocorreu um erro ao alterar o produto!" };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = false, Mensagem = "Ocorreu um erro ao alterar produtos!" };
            }
        }

        public async Task<BaseResponse> Deletar(int produtoId)
        {
            try
            {
                var existeProduto = await _ProdutoRepository.BuscarUm(produtoId);

                if (existeProduto == null)
                    return new BaseResponse() { Status = false, Mensagem = "Produto não encontrado!" };

                if (existeProduto.DthDelete != null)
                    return new BaseResponse() { Status = false, Mensagem = "Produto não encontrado!" };

                existeProduto.DthDelete = DateTime.Now;
                existeProduto.UsuarioIdDelete = _sessaoUsuario.GetId();
                existeProduto.UsuarioIdUpdate = _sessaoUsuario.GetId();
                existeProduto.DthUpdate = DateTime.Now;

                var retProduto = await _ProdutoRepository.Update(existeProduto);

                if (retProduto > 0)
                {
                    return new BaseResponse() { Status = true, Mensagem = "Produto deletado com sucesso!" };
                }
                else
                {
                    return new BaseResponse() { Status = false, Mensagem = "Ocorreu um erro ao deletar o produto!" };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = false, Mensagem = "Ocorreu um erro ao incluir produtos!" };
            }
        }
    }
}