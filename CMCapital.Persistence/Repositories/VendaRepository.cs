using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using CMCapital.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CMCapital.Persistence.Repositories
{
    public class VendaRepository : Repository<TblVendum>, IVendaRepository
    {
        private readonly CMCapitalDbContext _context;
        private readonly ILogger _logger;
        public VendaRepository(CMCapitalDbContext context, ILogger<VendaRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TblVendum>?> BuscarTodos()
        {
            try
            {
                return await _context.TblVenda.Where(v => v.DthDelete == null).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em {ClassName}: {ErrorMessage}", this.GetType().Name, ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }
    }
}