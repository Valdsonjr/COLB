using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using DataAccess.Entities;
using Services.Abstract;
using Services.Models;
using Services.Util;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Services.Concrete
{
    /// <summary>
    /// Serviços relacionados a requisições
    /// </summary>
    public class RequisicaoService : IRequisicaoService
    {
        private readonly Context context;
        private readonly IMapper mapper;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public RequisicaoService(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtém todas as requisições
        /// </summary>
        /// <returns>Lista de requisições</returns>
        IQueryable<GetRequisicaoModel> IRequisicaoService.Obter() => context.Requisicoes.ProjectTo<GetRequisicaoModel>(mapper.ConfigurationProvider);

        /// <summary>
        /// Cadastra uma nova requisição
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        GetRequisicaoModel IRequisicaoService.Inserir(PostRequisicaoModel model)
        {
            if (context.Requisicoes.Find(model.NrRequisicao) != null)
                throw new ApiException(HttpStatusCode.Conflict, new List<string> { "Requisição já cadastrada" }, $"Requisição já cadastrada: {model.NrRequisicao}");

            var requisicao = mapper.Map<PostRequisicaoModel, Requisicao>(model);

            context.Add<Requisicao>(requisicao);
            context.SaveChanges();

            return mapper.Map<Requisicao, GetRequisicaoModel>(requisicao);
        }
    }
}
