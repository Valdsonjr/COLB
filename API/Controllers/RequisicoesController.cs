using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    /// <summary>
    /// Endpoints relacionados a requisições de mudanças, incidentes e pequenos projetos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequisicoesController : ControllerBase
    {
        private readonly IRequisicaoService requisicaoService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="requisicaoService"></param>
        public RequisicoesController(IRequisicaoService requisicaoService)
        {
            this.requisicaoService = requisicaoService;
        }

        /// <summary>
        /// Cadastra uma nova requisição
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(GetRequisicaoModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status409Conflict)]
        public IActionResult Post(PostRequisicaoModel requisicao)
        {
            var retorno = requisicaoService.Inserir(requisicao);

            var ret = new
            {
                retorno.NrRequisicao,
                retorno.DsRequisicao,
                retorno.DtSolicitacao,
                retorno.NmSolicitante,
                OrdensDeLiberacao = retorno.OrdensDeLiberacao.Select(ol => new
                {
                    ol.NrOrdemDeLiberacao,
                    ol.ProjetosAfetados
                })
            };

            return Created($"{Request.Scheme}://{Request.Host}/odata/Requisicoes?$filter=NrRequisicao eq {retorno.NrRequisicao}&$top=1", ret);
        }
    }
}
