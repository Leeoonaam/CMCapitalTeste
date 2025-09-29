using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCapital.Application.Dtos.Response
{
    public class ClienteResponse
    {
        public required int ClienteId {  get; set; }
        public required string Nome { get; set; }
        public required decimal SaldoDisponivel { get; set; }
        public required DateTime DataCadastro { get; set; }
    }
}