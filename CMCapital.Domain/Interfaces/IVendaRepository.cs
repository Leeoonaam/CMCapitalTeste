using CMCapital.Domain.Entities;

namespace CMCapital.Domain.Interfaces
{
    public interface IVendaRepository : IBase<TblVendum>
    {
        Task<List<TblVendum>?> BuscarTodos();
        Task<TblVendum?> BuscarPorId(int clienteId);
        Task<TblVendum?> BuscarPorNome(string nome);
    }
}