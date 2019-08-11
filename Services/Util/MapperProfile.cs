using AutoMapper;
using DataAccess.Entities;
using Services.Models;
using System.Linq;

namespace Services.Util
{
    /// <summary>
    /// Classe de configuração do AutoMapper
    /// </summary>
    public static class MapperProfile
    {
        /// <summary>
        /// Configuração do AutoMapper
        /// </summary>
        /// <returns></returns>
        public static IMapper GetMapper()
        {
            return new MapperConfiguration(conf =>
            {
                conf.CreateMap<OrdemDeLiberacao, GetOrdemDeLiberacaoModel>().ForMember(dest => dest.ProjetosAfetados, opt => opt.MapFrom(src => src.ProjetosAfetados.Select(pa => pa.Projeto)));
                conf.CreateMap<PostOrdemDeLiberacaoModel, OrdemDeLiberacao>();
                conf.CreateMap<Requisicao, GetRequisicaoModel>();
                conf.CreateMap<PostRequisicaoModel, Requisicao>();
                conf.CreateMap<Projeto, GetProjetoModel>();
                conf.CreateMap<Solucao, GetSolucaoModel>();
                conf.CreateMap<PostProjetoAfetadoModel, ProjetoAfetado>();
                conf.CreateMap<Usuario, GetUsuarioModel>();
                conf.CreateMap<GetUsuarioModel, GetOutroUsuarioModel>();
                conf.CreateMap<Perfil, GetPerfilModel>();
            }).CreateMapper();
        }
    }
}
