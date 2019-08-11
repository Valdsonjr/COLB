using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Abstract;
using Services.Concrete;
using Services.Models;
using Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class RequisicaoServiceTest
    {
        private readonly DbContextOptions<Context> options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "RequisicaoServiceTest").Options;
        private readonly Requisicao req = new Requisicao
        {
            NrRequisicao = 0,
            NmSolicitante = "Nome Teste",
            DtSolicitacao = DateTime.Now,
            DsRequisicao = "Descricao Teste"
        };

        [TestInitialize]
        public void Seed()
        {
            using var context = new Context(options);
            context.Database.EnsureCreated();

            context.Add<Requisicao>(req);

            context.SaveChanges();
        }

        [TestCleanup]
        public void CleanUp()
        {
            using var context = new Context(options);
            context.Database.EnsureCreated();

            context.Remove(req);

            context.SaveChanges();
        }

        [TestMethod]
        public void InserirTest()
        {
            using var context = new Context(options);
            context.Database.EnsureCreated();

            IRequisicaoService service = new RequisicaoService(context, MapperProfile.GetMapper());

            var retorno = service.Inserir(new PostRequisicaoModel
            {
                NrRequisicao = 1,
                DsRequisicao = "Teste",
                DtSolicitacao = DateTime.Now,
                NmSolicitante = "Fulano",
                OrdensDeLiberacao = new HashSet<PostOrdemDeLiberacaoModel>
                    {
                        new PostOrdemDeLiberacaoModel { NrOrdemDeLiberacao = 0 },
                        new PostOrdemDeLiberacaoModel { NrOrdemDeLiberacao = 1, ProjetosAfetados = new HashSet<PostProjetoAfetadoModel>
                        {
                            new PostProjetoAfetadoModel { CdProjeto = -1 },
                            new PostProjetoAfetadoModel { CdProjeto = -2 },
                        }},
                    }
            });

            Assert.IsNotNull(retorno);
        }

        [TestMethod]
        [Description("Tenta inserir uma requisição com NrRequisicao já existente.")]
        public void NaoInserirTest()
        {
            using var context = new Context(options);
            IRequisicaoService service = new RequisicaoService(context, MapperProfile.GetMapper());

            try
            {
                var requisicao = new PostRequisicaoModel
                {
                    NrRequisicao = 0,
                    DsRequisicao = "Teste",
                    DtSolicitacao = DateTime.Now,
                    NmSolicitante = "Fulano",
                    OrdensDeLiberacao = new HashSet<PostOrdemDeLiberacaoModel>
                    {
                        new PostOrdemDeLiberacaoModel { NrOrdemDeLiberacao = 0 },
                        new PostOrdemDeLiberacaoModel { NrOrdemDeLiberacao = 1, ProjetosAfetados = new HashSet<PostProjetoAfetadoModel>
                        {
                            new PostProjetoAfetadoModel { CdProjeto = -1 },
                            new PostProjetoAfetadoModel { CdProjeto = -2 },
                        }},
                    }
                };
                var retorno = service.Inserir(requisicao);
                Assert.Fail();
            }
            catch (ApiException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void ObterTest()
        {
            using var context = new Context(options);
            context.Database.EnsureCreated();

            IRequisicaoService service = new RequisicaoService(context, MapperProfile.GetMapper());

            var req = service.Obter().Include(r => r.OrdensDeLiberacao).FirstOrDefault();
            Assert.IsNotNull(req);
        }
    }
}
