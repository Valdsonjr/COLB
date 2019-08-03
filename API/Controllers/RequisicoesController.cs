using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Abstract;
using Services.Models;
using System;
using System.Collections.Generic;
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
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(PostRequisicaoModel requisicao)
        {
            var retorno = requisicaoService.Inserir(requisicao);

            return Created($"{Request.Scheme}://{Request.Host}/odata/Requisicoes?$filter=NrRequisicao eq {retorno.NrRequisicao}&$top=1", retorno);
        }
    }
}
