using Services.Models;
using System.Linq;

namespace Services.Abstract
{
    /// <summary>
    /// Interface para serviços de soluções
    /// </summary>
    public interface ISolucaoService
    {
        /// <summary>
        /// Serviço de obter soluções
        /// </summary>
        /// <returns></returns>
        IQueryable<GetSolucaoModel> Obter();
    }
}
