using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCapital.Application.Dtos.Request
{
    public class AdicionarProdutoRequest
    {
        public string nome { get; set; }
        public decimal preco { get; set; }
        public int quantidade { get; set; }
    }
}
