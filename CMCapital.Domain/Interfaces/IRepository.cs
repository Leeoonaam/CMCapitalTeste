namespace CMCapital.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<int> Add(T entity);
        Task<int> AddRange(List<T> entity);
        Task<T> AddReturnEntity(T entity);
        Task<int> Update(T entity);
        Task<int> UpdateRange(List<T> entity);
        Task<bool> Delete(T entity);
        Task<bool> BeginTransaction();
        Task<bool> CommitTransaction();
        Task<bool> RollbackTransaction();
        bool Detach(T entity);
    }
}
