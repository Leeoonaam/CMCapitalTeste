using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCapital.Application.Dtos.Adicionar
{
    public class AdicionarVendaRequest
    {
        public required int ClienteId { get; set; }
        public required int ProdutoId { get; set; }
        public required int Quantidade { get; set; }

    }
}