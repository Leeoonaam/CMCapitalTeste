using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCapital.Application.Dtos.Request
{
    public class AdicionarProdutoRequest
    {
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }
        public required int Quantidade { get; set; }
    }
}
