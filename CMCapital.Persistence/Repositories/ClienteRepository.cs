using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using CMCapital.Persistence.Context;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CMCapital.Persistence.Repositories
{
    public class ClienteRepository : Repository<TblCliente>, IClienteRepository
    {
        private readonly CMCapitalDbContext _context;
        private readonly ILogger _logger;
        public ClienteRepository(CMCapitalDbContext context, ILogger<ClienteRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TblCliente?> BuscarPorNome(string nome)
        {
            try
            {
                return await _context.TblClientes.FirstOrDefaultAsync(c => c.DthDelete == null && c.Nome == nome);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em {ClassName}: {ErrorMessage}", this.GetType().Name, ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }
    }
}