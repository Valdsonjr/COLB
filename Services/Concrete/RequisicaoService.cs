﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Abstract;
using Services.Models;
using Services.Util;
using System;
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
                throw new ApiException(HttpStatusCode.Conflict, $"Requisição já cadastrada: {model.NrRequisicao}");

            var ols = context.OrdensDeLiberacao
                             .Where(ol => model.OrdensDeLiberacao
                                               .Select(o => o.NrOrdemDeLiberacao)
                                               .Contains(ol.NrOrdemDeLiberacao));

            if (ols.Any())
                throw new ApiException(HttpStatusCode.Conflict, 
                    $"Ordem(ns) de liberação já cadastrada(s): {String.Join(',', ols.Select(ol => ol.NrOrdemDeLiberacao))}");

            var requisicao = mapper.Map<PostRequisicaoModel, Requisicao>(model);

            context.Add<Requisicao>(requisicao);
            context.SaveChanges();

            var retorno = context.Requisicoes.Include(r => r.OrdensDeLiberacao)
                                             .ThenInclude(ol => ol.ProjetosAfetados)
                                             .ThenInclude(pa => pa.Projeto)
                                             .SingleOrDefault(r => r.NrRequisicao == model.NrRequisicao);

            return mapper.Map<Requisicao, GetRequisicaoModel>(retorno);
        }
    }
}
