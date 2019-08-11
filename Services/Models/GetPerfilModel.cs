using System;

namespace Services.Models
{
    /// <summary>
    /// Model de perfil do usuário
    /// </summary>
    public class GetPerfilModel
    {
        /// <summary>
        /// Código do perfil
        /// </summary>
        public Int32 CdPerfil { get; set; }

        /// <summary>
        /// Descrição do perfil
        /// </summary>
        public String DsPerfil { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public GetPerfilModel()
        {
            CdPerfil = 0;
            DsPerfil = "";
        }
    }
}
