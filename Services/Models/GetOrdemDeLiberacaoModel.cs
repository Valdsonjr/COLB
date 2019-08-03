using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public Int64 NrOrdemDeLiberacao { get; set; }
        /// <summary>
        /// Requisição a qual a ordem de liberação pertence
        /// </summary>
        [ForeignKey("NrRequicao")]
        public GetRequisicaoModel Requisicao { get; set; }
        /// <summary>
        /// Lista de projetos afetados pela ordem de liberação
        /// </summary>
        public IEnumerable<GetProjetoModel> ProjetosAfetados { get; set; }
    }
}