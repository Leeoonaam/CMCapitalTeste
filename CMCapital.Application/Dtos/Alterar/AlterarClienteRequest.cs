using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCapital.Application.Dtos.Alterar
{
    public class AlterarClienteRequest
    {
        public required int ClienteId { get; set; }
        public required string Nome { get; set; }
        public required decimal SaldoDisponivel { get; set; }
    }
}