using System;
using System.Collections.Generic;

namespace Services.Models
{
    /// <summary>
    /// Model para cadastro de nova requisição
    /// </summary>
    public class PostRequisicaoModel
    {
        /// <summary>
        /// Número da requisição
        /// </summary>
        public Int64 NrRequisicao { get; set; }
        /// <summary>
        /// Descrição da requisição
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
        /// Lista de ordens de liberação
        /// </summary>
        public ISet<PostOrdemDeLiberacaoModel> OrdensDeLiberacao { get; set; }
        /// <summary>
        /// Construtor
        /// </summary>
        public PostRequisicaoModel()
        {
            NrRequisicao = 0;
            DsRequisicao = "";
            DtSolicitacao = DateTime.MinValue;
            NmSolicitante = "";
            OrdensDeLiberacao = new HashSet<PostOrdemDeLiberacaoModel>();
        }
    }
}
