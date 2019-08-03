using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    /// <summary>
    /// Model de requisição
    /// </summary>
    public class GetRequisicaoModel
    {
        /// <summary>
        /// Número da requisição
        /// </summary>
        [Key]
        public Int64 NrRequisicao { get; set; }

        /// <summary>
        /// Descrição da requisção
        /// </summary>
        public String DsRequisicao { get; set; }

        /// <summary>
        /// Data de criação da requisição
        /// </summary>
        public DateTime DtSolicitacao { get; set; }

        /// <summary>
        /// Nome do solicitante
        /// </summary>
        public String NmSolicitante { get; set; }

        /// <summary>
        /// Ordens de liberação relacionadas à requisição
        /// </summary>
        public IEnumerable<GetOrdemDeLiberacaoModel> OrdensDeLiberacao { get; set; }
    }
}
