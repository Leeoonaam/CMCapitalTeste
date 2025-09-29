using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCapital.Application.Dtos.Response
{
    public class ProdutoResponse
    {
        public int produtoId { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public int quantidade { get; set; }
    }
}
