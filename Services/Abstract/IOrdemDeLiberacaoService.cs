using Services.Models;
using System.Linq;

namespace Services.Abstract
{
    /// <summary>
    /// Interface para serviços de ordens de liberação
    /// </summary>
    public interface IOrdemDeLiberacaoService
    {
        /// <summary>
        /// Serviço para obter as ordens de liberação cadastradas
        /// </summary>
        /// <returns></returns>
        IQueryable<GetOrdemDeLiberacaoModel> Obter();
    }
}
