using CMCapital.Domain.Entities;

namespace CMCapital.Domain.Interfaces
{
    public interface IUsuarioRepository : IBase<TblUsuario>
    {
        Task<TblUsuario?> ObterUsuarioPorCpfESenha(string cpf, string senha);
    }
}