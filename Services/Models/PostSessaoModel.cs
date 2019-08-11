using Services.Util;
using System;

namespace Services.Models
{
    /// <summary>
    /// Login
    /// </summary>
    public class PostSessaoModel
    {
        /// <summary>
        /// Email
        /// </summary>
        public Email Email { get; }
        /// <summary>
        /// Senha
        /// </summary>
        public Password Senha { get; }
        /// <summary>
        /// Construtor
        /// </summary>
        public PostSessaoModel(String email, String senha)
        {
            Email = new Email(email);
            Senha = new Password(senha);
        }
    }
}
