using DataAccess;
using DataAccess.Entities;
using Services.Abstract;
using System.Linq;

namespace Services.Concrete
{
    /// <summary>
    /// Serviços relacionados a ordens de liberação
    /// </summary>
    public class OrdemDeLiberacaoService : IOrdemDeLiberacaoService
    {
        private readonly Context context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public OrdemDeLiberacaoService(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Obtém todas as ordens de liberação cadastradas
        /// </summary>
        /// <returns></returns>
        public IQueryable<OrdemDeLiberacao> Obter() => context.OrdensDeLiberacao;
    }
}
