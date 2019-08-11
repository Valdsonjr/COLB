using System;

namespace Services.Models
{
    /// <summary>
    /// Model para retorno do login
    /// </summary>
    public class GetSessaoModel
    {
        /// <summary>
        /// Id do funcionário
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// Nome do funcionário
        /// </summary>
        public String Nome { get; set; }
        /// <summary>
        /// Token de acesso
        /// </summary>
        public String Token { get; set; }
        /// <summary>
        /// Descrição do perfil do usuário
        /// </summary>
        public String Perfil { get; set; }
        /// <summary>
        /// Construtor
        /// </summary>
        public GetSessaoModel()
        {
            Id = 0;
            Nome = "";
            Token = "";
            Perfil = "";
        }
    }
}
