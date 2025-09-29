using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCapital.Application.Dtos.Request
{
    public class LoginRequest
    {
        public required string cpf { get; set; }
        public required string senha { get; set; }
    }
}