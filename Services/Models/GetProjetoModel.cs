using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
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
        [ForeignKey("CdSolucao")]
        public GetSolucaoModel Solucao { get; set; }
    }
}
