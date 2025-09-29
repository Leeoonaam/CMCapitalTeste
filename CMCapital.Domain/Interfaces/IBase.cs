namespace CMCapital.Domain.Interfaces
{
    public interface IBase<T> : IRepository<T> where T : class
    {

    }
}