using CMCapital.Domain.Entities;

namespace CMCapital.Domain.Interfaces
{
    public interface IClienteRepository : IBase<TblCliente>
    {
        Task<TblCliente?> BuscarPorNome(string nome);
    }
}