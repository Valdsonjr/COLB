using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System.Linq;

namespace API.Controllers
{
    /// <summary>
    /// Endpoints relacionados aos projetos do Visual Studio
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly IProjetoService projetoService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="projetoService"></param>
        public ProjetosController(IProjetoService projetoService)
        {
            this.projetoService = projetoService;
        }
    }
}