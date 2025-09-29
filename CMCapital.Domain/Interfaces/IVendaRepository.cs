using CMCapital.Domain.Entities;

namespace CMCapital.Domain.Interfaces
{
    public interface IVendaRepository : IBase<TblVendum>
    {
        Task<List<TblVendum>?> BuscarTodos();
    }
}