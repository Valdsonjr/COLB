using DataAccess.Entities;
using Services.Models;
using Services.Util;
using System.Linq;

namespace Services.Abstract
{
    /// <summary>
    /// Interface para serviços de requisições
    /// </summary>
    public interface IRequisicaoService
    {
        /// <summary>
        /// Serviço de obter requisições
        /// </summary>
        /// <returns></returns>
        IQueryable<GetRequisicaoModel> Obter();

        /// <summary>
        /// Serviço de cadastrar novas requisições
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        GetRequisicaoModel Inserir(PostRequisicaoModel model);
    }
}
