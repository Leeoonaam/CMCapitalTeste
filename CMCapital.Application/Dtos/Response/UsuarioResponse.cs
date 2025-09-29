using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCapital.Application.Dtos.Response
{
    public class UsuarioResponse
    {
        public string? CPF { get; set; }
        public DateTime DataCadastro { get; set; }        
    }
}