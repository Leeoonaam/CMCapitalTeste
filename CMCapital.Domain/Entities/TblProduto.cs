using System;
using System.Collections.Generic;

namespace CMCapital.Domain.Entities;

public partial class TblProduto
{
    public int ProdutoId { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public int Quantidade { get; set; }

    public int UsuarioIdInsert { get; set; }

    public int? UsuarioIdUpdate { get; set; }

    public int? UsuarioIdDelete { get; set; }

    public DateTime DthInsert { get; set; }

    public DateTime? DthUpdate { get; set; }

    public DateTime? DthDelete { get; set; }

    public virtual ICollection<TblVendum> TblVenda { get; set; } = new List<TblVendum>();
}
