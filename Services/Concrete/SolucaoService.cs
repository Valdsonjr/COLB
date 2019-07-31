using DataAccess;
using DataAccess.Entities;
using Services.Abstract;
using System.Linq;

namespace Services.Concrete
{
    /// <summary>
    /// Serviços relacionados a soluções do Visual Studio
    /// </summary>
    public class SolucaoService : ISolucaoService
    {
        private readonly Context context;

        /// <summary>
        /// Construtor 
        /// </summary>
        /// <param name="context"></param>
        public SolucaoService(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Obtém todas as soluções cadastradas
        /// </summary>
        /// <returns>Lista de soluções</returns>
        public IQueryable<Solucao> Obter() => context.Solucoes;
    }
}
