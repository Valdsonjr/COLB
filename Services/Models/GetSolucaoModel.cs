using System;
using System.Collections.Generic;

namespace Services.Models
{
    /// <summary>
    /// Model de soluções do Visual Studio
    /// </summary>
    public class GetSolucaoModel
    {
        /// <summary>
        /// Código da solução
        /// </summary>
        public Int32 CdSolucao { get; set; }
        /// <summary>
        /// Nome (como aparece no Visual Studio) da solução
        /// </summary>
        public String NmSolucao { get; set; }
        /// <summary>
        /// Descrição da solução
        /// </summary>
        public String DsSolucao { get; set; }
        /// <summary>
        /// Projetos do Visual Studio contidos na solução 
        /// </summary>
        public IEnumerable<GetProjetoModel> Projetos { get; set; }
        /// <summary>
        /// Construtor
        /// </summary>
        public GetSolucaoModel()
        {
            CdSolucao = 0;
            NmSolucao = "";
            DsSolucao = "";
            Projetos = new List<GetProjetoModel>();
        }
    }
}
