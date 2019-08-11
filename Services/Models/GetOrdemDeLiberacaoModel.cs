using System;
using System.Collections.Generic;

namespace Services.Models
{
    /// <summary>
    /// Model de Ordem de liberação
    /// </summary>
    public class GetOrdemDeLiberacaoModel
    {
        /// <summary>
        /// Número da ordem de liberação
        /// </summary>
        public Int64 NrOrdemDeLiberacao { get; set; }
        /// <summary>
        /// Requisição a qual a ordem de liberação pertence
        /// </summary>
        public GetRequisicaoModel Requisicao { get; set; }
        /// <summary>
        /// Lista de projetos afetados pela ordem de liberação
        /// </summary>
        public IEnumerable<GetProjetoModel> ProjetosAfetados { get; set; }
        /// <summary>
        /// Construtor
        /// </summary>
        public GetOrdemDeLiberacaoModel()
        {
            NrOrdemDeLiberacao = 0;
            Requisicao = new GetRequisicaoModel();
            ProjetosAfetados = new List<GetProjetoModel>();
        }
    }
}