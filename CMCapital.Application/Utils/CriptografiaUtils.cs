using CMCapital.Application.Dtos.Enums;
using CMCapital.Application.Dtos.Response;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CMCapital.Application.Utils
{
    public class CriptografiaUtils
    {
        public static string GerarUUID()
        {
            Guid uuid = Guid.NewGuid();
            string stringUUID = uuid.ToString();

            return stringUUID.ToUpper();
        }
        public static string CriptografarSenha(string textoParaCriptografar)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(textoParaCriptografar));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string GerarToken(SessaoUsuarioResponse sessao, string hash)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(hash);
            var token = new SecurityTokenDescriptor();
            token.Expires = sessao.VencimentoSessao;

            var sessaoClaim = new Claim[]
            {
                new Claim(ClaimTypes.Sid, sessao.UsuarioId.ToString()),
                new Claim(ClaimTypes.SerialNumber, sessao.CPF!),
                new Claim(ClaimTypes.Role, RolesAuthorize.UsuarioRole),
                new Claim("CPF", sessao.CPF ?? ""),
                new Claim("VencimentoSessao", token.Expires.ToString() ?? "")
            };

            var claims = new ClaimsIdentity(sessaoClaim);

            token.Subject = claims;

            token.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenAuth = tokenHandler.CreateToken(token);
            return tokenHandler.WriteToken(tokenAuth);
        }

        public static string CriptografarString(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32, '\0').Substring(0, 32));
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    byte[] encryptedBytes = msEncrypt.ToArray();
                    return ToBase64UrlString(encryptedBytes);
                }
            }
        }
        public static string DescriptografarString(string cipherText, string key)
        {
            byte[] cipherBytes = FromBase64UrlString(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32, '\0').Substring(0, 32));
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static string ToBase64UrlString(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }
        public static byte[] FromBase64UrlString(string input)
        {
            string base64 = input
                .Replace('-', '+')
                .Replace('_', '/')
                + new string('=', (4 - input.Length % 4) % 4);

            return Convert.FromBase64String(base64);
        }
    }
}