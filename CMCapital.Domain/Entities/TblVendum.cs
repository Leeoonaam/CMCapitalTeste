using System;
using System.Collections.Generic;

namespace CMCapital.Domain.Entities;

public partial class TblVendum
{
    public int VendaId { get; set; }

    public int ClienteId { get; set; }

    public int ProdutoId { get; set; }

    public int Quantidade { get; set; }

    public int UsuarioIdInsert { get; set; }

    public int? UsuarioIdUpdate { get; set; }

    public int? UsuarioIdDelete { get; set; }

    public DateTime DthInsert { get; set; }

    public DateTime? DthUpdate { get; set; }

    public DateTime? DthDelete { get; set; }

    public virtual TblCliente Cliente { get; set; } = null!;

    public virtual TblProduto Produto { get; set; } = null!;
}
