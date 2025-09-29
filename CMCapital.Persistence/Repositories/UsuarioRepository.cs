using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using CMCapital.Persistence.Context;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CMCapital.Persistence.Repositories
{
    public class UsuarioRepository : Repository<TblUsuario>, IUsuarioRepository
    {
        private readonly CMCapitalDbContext _context;
        private readonly ILogger _logger;
        public UsuarioRepository(CMCapitalDbContext context, ILogger<UsuarioRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TblUsuario?> ObterUsuarioPorCpfESenha(string cpf, string senha)
        {
            try
            {
                return await _context.TblUsuarios.FirstOrDefaultAsync(u => u.Cpf == cpf && u.Senha == senha);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em {ClassName}: {ErrorMessage}", this.GetType().Name, ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }
    }
}