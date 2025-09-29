using CMCapital.Domain.Entities;

namespace CMCapital.Domain.Interfaces
{
    public interface IProdutoRepository : IBase<TblProduto>
    {
        Task<List<TblProduto>?> BuscarTodos();
        Task<TblProduto?> BuscarUmPorNome(string nome);
        Task<TblProduto?> BuscarUm(int id);
    }
}