using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Services.Abstract;
using Services.Models;
using System.Linq;

namespace Services.Concrete
{
    /// <summary>
    /// Serviços relacionados a ordens de liberação
    /// </summary>
    public class OrdemDeLiberacaoService : IOrdemDeLiberacaoService
    {
        private readonly Context context;
        private readonly IMapper mapper;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public OrdemDeLiberacaoService(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        IQueryable<GetOrdemDeLiberacaoModel> IOrdemDeLiberacaoService.Obter() => context.OrdensDeLiberacao.ProjectTo<GetOrdemDeLiberacaoModel>(mapper.ConfigurationProvider);
    }
}
