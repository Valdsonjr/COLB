using System;

namespace Services.Models
{
    /// <summary>
    /// Model para obter usuário
    /// </summary>
    public class GetUsuarioModel
    {
        /// <summary>
        /// Código do usuário
        /// </summary>
        public Int32 CdUsuario { get; set; }
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public String NmUsuario { get; set; }
        /// <summary>
        /// Email do usuário
        /// </summary>
        public String DsEmail { get; set; }
        /// <summary>
        /// Telefone do usuário
        /// </summary>
        public Int64 NrTelefone { get; set; }
        /// <summary>
        /// Data de cadastro do usuário
        /// </summary>
        public DateTime DtCadastro { get; set; }
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
        public GetUsuarioModel()
        {
            CdUsuario = 0;
            NmUsuario = "";
            DsEmail = "";
            NrTelefone = 0;
            DtCadastro = DateTime.MinValue;
            DtNascimento = DateTime.MinValue;
            Perfil = new GetPerfilModel();
        }
    }
}
