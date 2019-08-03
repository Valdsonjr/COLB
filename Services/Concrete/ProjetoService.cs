using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Services.Abstract;
using Services.Models;
using System.Linq;

namespace Services.Concrete
{
    /// <summary>
    /// Serviços relacionados a projetos do Visual Studio
    /// </summary>
    public class ProjetoService : IProjetoService
    {
        private readonly Context context;
        private readonly IMapper mapper;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public ProjetoService(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        IQueryable<GetProjetoModel> IProjetoService.Obter() => context.Projetos.ProjectTo<GetProjetoModel>(mapper.ConfigurationProvider);
    }
}
