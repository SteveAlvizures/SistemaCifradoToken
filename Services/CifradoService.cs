using System.Security.Cryptography;
using System.Text;

namespace SistemaCifradoToken.Services
{
    public class CifradoService
    {
        private readonly byte[] _key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
        private readonly byte[] _iv = Encoding.UTF8.GetBytes("1234567890123456");

        public string Cifrar(string textoPlano)
        {
            using Aes aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using MemoryStream ms = new MemoryStream();
            using CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(textoPlano);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Descifrar(string textoCifrado)
        {
            using Aes aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using MemoryStream ms = new MemoryStream(Convert.FromBase64String(textoCifrado));
            using CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}