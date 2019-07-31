using DataAccess;
using DataAccess.Entities;
using Services.Abstract;
using System.Linq;

namespace Services.Concrete
{
    /// <summary>
    /// Serviços relacionados a projetos do Visual Studio
    /// </summary>
    public class ProjetoService : IProjetoService
    {
        private readonly Context context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public ProjetoService(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Obtém todos os projetos cadastrados
        /// </summary>
        /// <returns></returns>
        public IQueryable<Projeto> Obter() => context.Projetos;
    }
}
