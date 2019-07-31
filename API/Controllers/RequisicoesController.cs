using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Abstract;
using Services.Models;
using System;
using System.Reflection;

namespace API.Controllers
{
    /// <summary>
    /// Endpoints relacionados a requisições de mudanças, incidentes e pequenos projetos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RequisicoesController : ControllerBase
    {
        private readonly IRequisicaoService requisicaoService;
        private readonly ILogger<RequisicoesController> logger;
        private readonly string erro = "Ocorreu um erro inesperado, por favor contacte o administrador do sistema.";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requisicaoService"></param>
        /// <param name="logger"></param>
        public RequisicoesController(IRequisicaoService requisicaoService, ILogger<RequisicoesController> logger)
        {
            this.requisicaoService = requisicaoService;
            this.logger = logger;
        }

        /// <summary>
        /// Cadastra uma nova requisição
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PostRequisicaoModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(PostRequisicaoModel requisicao)
        {
            try
            {
                var retorno = requisicaoService.Inserir(requisicao);
                return retorno.Either<IActionResult>(e => Conflict(e),
                                                     o => Created($"{Request.Scheme}://{Request.Host}/odata/Requisicoes?$filter=NrRequisicao eq {o.NrRequisicao}&$top=1", requisicao));
            }
            catch (Exception exception)
            {
                logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType.Name}] - {MethodBase.GetCurrentMethod().Name}", exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, erro);
            }
        }
    }
}
