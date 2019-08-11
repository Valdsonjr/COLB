using System;
using System.Security.Cryptography;
using System.Text;

namespace Services.Util
{
    /// <summary>
    /// Senha
    /// </summary>
    public class Password
    {
        /// <summary>
        /// Valor cru
        /// </summary>
        public Byte[] Raw { get; }
        
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="value"></param>
        public Password(String value)
        {
            using var sha = SHA512.Create();
            Raw = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}
