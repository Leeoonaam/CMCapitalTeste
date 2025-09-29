using CMCapital.Domain.Interfaces;
using CMCapital.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CMCapital.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CMCapitalDbContext _context;
        private readonly ILogger<Repository<T>> _logger;

        public Repository(CMCapitalDbContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> Add(T entity)
        {
            try
            {
                _context.Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar entidade {EntityType}", typeof(T).Name);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<int> AddRange(List<T> entities)
        {
            try
            {
                await _context.AddRangeAsync(entities);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar lista de entidades {EntityType}", typeof(T).Name);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<T> AddReturnEntity(T entity)
        {
            try
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar e retornar entidade {EntityType}", typeof(T).Name);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<int> Update(T entity)
        {
            try
            {
                _context.Update(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar entidade {EntityType}", typeof(T).Name);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<int> UpdateRange(List<T> entities)
        {
            try
            {
                _context.UpdateRange(entities);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar lista de entidades {EntityType}", typeof(T).Name);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                _context.Remove(entity);
                return await _context.SaveChangesAsync() != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar entidade {EntityType}", typeof(T).Name);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<bool> BeginTransaction()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao iniciar transação");
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<bool> CommitTransaction()
        {
            try
            {
                await _context.Database.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar commit da transação");
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<bool> RollbackTransaction()
        {
            try
            {
                await _context.Database.RollbackTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar rollback da transação");
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public bool Detach(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Detached;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar detach da entidade {EntityType}", typeof(T).Name);
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }
    }
}
