using DataAccess.Entities;
using System.Linq;

namespace Services.Abstract
{
    /// <summary>
    /// Interface para serviços de projetos do Visual Studio 
    /// </summary>
    public interface IProjetoService
    {
        /// <summary>
        /// Serviço para obter projetos do Visual Studio
        /// </summary>
        /// <returns></returns>
        IQueryable<Projeto> Obter();
    }
}
