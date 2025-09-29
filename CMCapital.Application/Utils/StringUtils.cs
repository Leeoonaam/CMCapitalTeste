using System.Text.RegularExpressions;

namespace CMCapital.Application.Utils
{
    public class StringUtils
    {
        public static string PegarNumeros(string texto)
        {
            return Regex.Replace(texto, "\\D", "");
        }
        public static string PegarLetrasEhNumeros(string texto)
        {
            return Regex.Replace(texto, "[^a-zA-Z0-9]", "");
        }

        public static string FormatarCpf(string cpf)
        {
            var numeros = PegarNumeros(cpf);
            if (numeros.Length != 11) return cpf;

            return Regex.Replace(numeros, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
        }

        public static string FormatarCnpj(string cnpj)
        {
            var numeros = PegarNumeros(cnpj);
            if (numeros.Length != 14) return cnpj;

            return Regex.Replace(numeros, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
        }

        public static string FormatarRg(string rg)
        {
            if (string.IsNullOrWhiteSpace(rg))
                return string.Empty;

            var rgFormatado = Regex.Replace(rg.Trim(), @"[^a-zA-Z0-9]", "");

            if (rgFormatado.Length >= 9)
            {
                return Regex.Replace(rgFormatado, @"(\d{2})(\d{3})(\d{3})([a-zA-Z0-9]{1})", "$1.$2.$3-$4");
            }

            return rgFormatado;
        }

        public static string FormatarCelular(string celular)
        {
            var numeros = PegarNumeros(celular);

            if (numeros.Length == 11)
                return Regex.Replace(numeros, @"(\d{2})(\d{5})(\d{4})", "($1) $2-$3");
            if (numeros.Length == 10)
                return Regex.Replace(numeros, @"(\d{2})(\d{4})(\d{4})", "($1) $2-$3");

            return celular;
        }
    }
}
