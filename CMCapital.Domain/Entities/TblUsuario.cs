using System;
using System.Collections.Generic;

namespace CMCapital.Domain.Entities;

public partial class TblUsuario
{
    public int UsuarioId { get; set; }

    public string Cpf { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public int UsuarioIdInsert { get; set; }

    public int? UsuarioIdUpdate { get; set; }

    public int? UsuarioIdDelete { get; set; }

    public DateTime DthInsert { get; set; }

    public DateTime? DthUpdate { get; set; }

    public DateTime? DthDelete { get; set; }
}
