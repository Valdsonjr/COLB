using DataAccess.Entities;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System.Linq;

namespace API.Controllers
{
    /// <summary>
    /// Endpoints OData (classe temporária até controllers OData aparecerem no swagger)
    /// </summary>
    public class ODataQueriesController : ODataController
    {
        private readonly IRequisicaoService requisicaoService;
        private readonly IOrdemDeLiberacaoService ordemDeLiberacaoService;
        private readonly ISolucaoService solucaoService;
        private readonly IProjetoService projetoService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="requisicaoService"></param>
        /// <param name="ordemDeLiberacaoService"></param>
        /// <param name="solucaoService"></param>
        /// <param name="projetoService"></param>
        public ODataQueriesController(IRequisicaoService requisicaoService, 
                                      IOrdemDeLiberacaoService ordemDeLiberacaoService, 
                                      ISolucaoService solucaoService, 
                                      IProjetoService projetoService)
        {
            this.requisicaoService = requisicaoService;
            this.ordemDeLiberacaoService = ordemDeLiberacaoService;
            this.solucaoService = solucaoService;
            this.projetoService = projetoService;
        }

        /// <summary>
        /// OData a partir das requisições
        /// </summary>
        /// <returns></returns>
        [ODataRoute("Requisicoes"), EnableQuery]
        public ActionResult<IQueryable<Requisicao>> GetRequisicoes() => Ok(requisicaoService.Obter());

        /// <summary>
        /// OData a partir dos projetos
        /// </summary>
        /// <returns></returns>
        [ODataRoute("Projetos"), EnableQuery]
        public ActionResult<IQueryable<Projeto>> GetProjetos() => Ok(projetoService.Obter());

        /// <summary>
        /// OData a partir das soluções
        /// </summary>
        /// <returns></returns>
        [ODataRoute("Solucoes"), EnableQuery]
        public ActionResult<IQueryable<Projeto>> GetSolucoes() => Ok(solucaoService.Obter());

        /// <summary>
        /// OData a partir das ordens de liberação
        /// </summary>
        /// <returns></returns>
        [ODataRoute("OrdensDeLiberacao"), EnableQuery]
        public ActionResult<IQueryable<OrdemDeLiberacao>> GetOrdensDeLiberacao() => Ok(ordemDeLiberacaoService.Obter());
    }
}