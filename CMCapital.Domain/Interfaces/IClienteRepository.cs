using CMCapital.Domain.Entities;

namespace CMCapital.Domain.Interfaces
{
    public interface IClienteRepository : IBase<TblCliente>
    {
        Task<List<TblCliente>?> BuscarTodos();
        Task<TblCliente?> BuscarPorId(int clienteId);
        Task<TblCliente?> BuscarPorNome(string nome);
    }
}