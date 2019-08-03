using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Services.Abstract;
using Services.Models;
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
        public IQueryable<GetRequisicaoModel> GetRequisicoes() => requisicaoService.Obter();

        /// <summary>
        /// OData a partir dos projetos
        /// </summary>
        /// <returns></returns>
        [ODataRoute("Projetos"), EnableQuery]
        public IQueryable<GetProjetoModel> GetProjetos() => projetoService.Obter();

        /// <summary>
        /// OData a partir das soluções
        /// </summary>
        /// <returns></returns>
        [ODataRoute("Solucoes"), EnableQuery]
        public IQueryable<GetSolucaoModel> GetSolucoes() => solucaoService.Obter();

        /// <summary>
        /// OData a partir das ordens de liberação
        /// </summary>
        /// <returns></returns>
        [ODataRoute("OrdensDeLiberacao"), EnableQuery]
        public IQueryable<GetOrdemDeLiberacaoModel> GetOrdensDeLiberacao() => ordemDeLiberacaoService.Obter();
    }
}