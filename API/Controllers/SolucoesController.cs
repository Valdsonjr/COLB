using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System.Linq;

namespace API.Controllers
{
    /// <summary>
    /// Endpoints relacionados a soluções do Visual Studio
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SolucoesController : ControllerBase
    {
        private readonly ISolucaoService solucaoService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="solucaoService"></param>
        public SolucoesController(ISolucaoService solucaoService)
        {
            this.solucaoService = solucaoService;
        }
    }
}