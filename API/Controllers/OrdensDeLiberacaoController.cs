using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System.Linq;

namespace API.Controllers
{
    /// <summary>
    /// Endpoints relacionados a ordens de liberação
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdensDeLiberacaoController : ControllerBase
    {
        private readonly IOrdemDeLiberacaoService ordemDeLiberacaoService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="ordemDeLiberacaoService"></param>
        public OrdensDeLiberacaoController(IOrdemDeLiberacaoService ordemDeLiberacaoService)
        {
            this.ordemDeLiberacaoService = ordemDeLiberacaoService;
        }
    }
}