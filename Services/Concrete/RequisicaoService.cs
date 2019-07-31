using DataAccess;
using DataAccess.Entities;
using Services.Abstract;
using Services.Models;
using Services.Util;
using System.Linq;

namespace Services.Concrete
{
    /// <summary>
    /// Serviços relacionados a requisições
    /// </summary>
    public class RequisicaoService : IRequisicaoService
    {
        private readonly Context context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public RequisicaoService(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Obtém todas as requisições
        /// </summary>
        /// <returns>Lista de requisições</returns>
        public IQueryable<Requisicao> Obter() => context.Requisicoes;

        /// <summary>
        /// Cadastra uma nova requisição
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<string, Requisicao> Inserir(PostRequisicaoModel model)
        {
            if (context.Requisicoes.Find(model.NrRequisicao) != null)
                return new Result<string, Requisicao>("Requisição já cadastrada");

            var requisicao = new Requisicao
            {
                NrRequisicao = model.NrRequisicao,
                DsRequisicao = model.DsRequisicao,
                DtSolicitacao = model.DtSolicitacao,
                NmSolicitante = model.NmSolicitante
            };

            context.Add<Requisicao>(requisicao);
            context.SaveChanges();
            return new Result<string, Requisicao>(requisicao);
        }
    }
}
