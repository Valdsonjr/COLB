using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Services.Abstract;
using Services.Models;
using System.Linq;

namespace Services.Concrete
{
    /// <summary>
    /// Serviços relacionados a soluções do Visual Studio
    /// </summary>
    public class SolucaoService : ISolucaoService
    {
        private readonly Context context;
        private readonly IMapper mapper;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public SolucaoService(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtém todas as soluções cadastradas
        /// </summary>
        /// <returns>Lista de soluções</returns>
        IQueryable<GetSolucaoModel> ISolucaoService.Obter() => context.Solucoes.ProjectTo<GetSolucaoModel>(mapper.ConfigurationProvider);
    }
}
