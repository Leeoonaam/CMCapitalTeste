using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using CMCapital.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CMCapital.Persistence.Repositories
{
    public class ProdutoRepository : Repository<TblProduto>, IProdutoRepository
    {
        private readonly CMCapitalDbContext _context;
        private readonly ILogger _logger;
        public ProdutoRepository(CMCapitalDbContext context, ILogger<ProdutoRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TblProduto>> BuscarTodos()
        {
            try
            {
                return await _context.TblProdutos.Where(p => p.DthDelete == null).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em {ClassName}: {ErrorMessage}", this.GetType().Name, ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<TblProduto> BuscarUmPorNome(string nome)
        {
            try
            {
                return await _context.TblProdutos.Where(p => p.Nome == nome).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em {ClassName}: {ErrorMessage}", this.GetType().Name, ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<TblProduto> BuscarUm(int id)
        {
            try
            {
                return await _context.TblProdutos.Where(p => p.ProdutoId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em {ClassName}: {ErrorMessage}", this.GetType().Name, ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }
    }
}