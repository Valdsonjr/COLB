using System;

namespace Services.Models
{
    /// <summary>
    /// Model de projetos do Visual Studio
    /// </summary>
    public class GetProjetoModel
    {
        /// <summary>
        /// Código do projeto
        /// </summary>
        public Int32 CdProjeto { get; set; }
        /// <summary>
        /// Nome (como aparece no Visual Studio) do projeto
        /// </summary>
        public String NmProjeto { get; set; }
        /// <summary>
        /// Descrição do projeto
        /// </summary>
        public String DsProjeto { get; set; }
        /// <summary>
        /// Solução (do Visual Studio) à qual o projeto pertence
        /// </summary>
        public GetSolucaoModel Solucao { get; set; }
        /// <summary>
        /// Construtor
        /// </summary>
        public GetProjetoModel()
        {
            NmProjeto = "";
            DsProjeto = "";
            Solucao = new GetSolucaoModel();
        }
    }
}
