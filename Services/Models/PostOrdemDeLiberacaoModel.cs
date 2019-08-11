using System;
using System.Collections.Generic;

namespace Services.Models
{
    /// <summary>
    /// Model de cadastro de ordem de liberação
    /// </summary>
    public class PostOrdemDeLiberacaoModel
    {
        /// <summary>
        /// Número da ordem de liberação
        /// </summary>
        public Int64 NrOrdemDeLiberacao { get; set; }
        /// <summary>
        /// Lista de ids de projetos afetados
        /// </summary>
        public ISet<PostProjetoAfetadoModel> ProjetosAfetados { get; set; }
        /// <summary>
        /// Construtor
        /// </summary>
        public PostOrdemDeLiberacaoModel()
        {
            NrOrdemDeLiberacao = 0;
            ProjetosAfetados = new HashSet<PostProjetoAfetadoModel>();
        }
    }
}
