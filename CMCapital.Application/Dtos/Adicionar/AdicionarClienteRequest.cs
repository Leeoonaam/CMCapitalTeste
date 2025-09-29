using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCapital.Application.Dtos.Adicionar
{
    public class AdicionarClienteRequest
    {
        public required string Nome { get; set; }
        public required decimal SaldoDisponivel { get; set; }
    }
}