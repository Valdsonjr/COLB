using AutoMapper;
using DataAccess.Entities;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                conf.CreateMap<OrdemDeLiberacao, GetOrdemDeLiberacaoModel>().ForMember(model => model.ProjetosAfetados, opt => opt.MapFrom(x => x.ProjetosAfetados.Select(y => y.Projeto)));
                conf.CreateMap<PostOrdemDeLiberacaoModel, OrdemDeLiberacao>();
                conf.CreateMap<Requisicao, GetRequisicaoModel>();
                conf.CreateMap<PostRequisicaoModel, Requisicao>();
                conf.CreateMap<Projeto, GetProjetoModel>();
                conf.CreateMap<Solucao, GetSolucaoModel>();
                conf.CreateMap<PostProjetoAfetadoModel, ProjetoAfetado>();
            }).CreateMapper();
        }
    }
}
