using System;

namespace Services.Models
{
    /// <summary>
    /// Model para obter informações públicas de um usuário
    /// </summary>
    public class GetOutroUsuarioModel
    {
        /// <summary>
        /// Id do usuário
        /// </summary>
        public Int32 CdUsuario { get; set; }
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public String NmUsuario { get; set; }
        /// <summary>
        /// Data de nascimento do usuário
        /// </summary>
        public DateTime DtNascimento { get; set; }
        /// <summary>
        /// Perfil do usuário
        /// </summary>
        public GetPerfilModel Perfil { get; set; }
        /// <summary>
        /// Construtor
        /// </summary>
        public GetOutroUsuarioModel()
        {
            CdUsuario = 0;
            NmUsuario = "";
            DtNascimento = DateTime.MinValue;
            Perfil = new GetPerfilModel();
        }
    }
}
