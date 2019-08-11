using Services.Util;
using System;

namespace Services.Models
{
    /// <summary>
    /// Model para cadastro de usuário
    /// </summary>
    public class PostUsuarioModel
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public String NmUsuario { get; }
        /// <summary>
        /// Endereço de e-mail do usuário
        /// </summary>
        public Email DsEmail { get; }
        /// <summary>
        /// Número de telefone do usuário
        /// </summary>
        public Int64 NrTelefone { get; }
        /// <summary>
        /// Data de nascimento do usuário
        /// </summary>
        public DateTime DtNascimento { get; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public Password DsSenha { get; }
        /// <summary>
        /// Construtor
        /// </summary>
        public PostUsuarioModel(String nmUsuario, String dsEmail, Int64 nrTelefone, DateTime dtNascimento, String dsSenha)
        {
            NmUsuario = nmUsuario;
            DsEmail = new Email(dsEmail);
            NrTelefone = nrTelefone;
            DtNascimento = dtNascimento;
            DsSenha = new Password(dsSenha);
        }
    }
}
