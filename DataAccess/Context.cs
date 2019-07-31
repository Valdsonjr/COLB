using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) :base(options) { }

        public DbSet<Requisicao> Requisicoes { get; set; }
        public DbSet<OrdemDeLiberacao> OrdensDeLiberacao { get; set; }
        public DbSet<Solucao> Solucoes { get; set; }
        public DbSet<Projeto> Projetos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Requisicao.Config(modelBuilder);
            OrdemDeLiberacao.Config(modelBuilder);
            Solucao.Config(modelBuilder);
            Projeto.Config(modelBuilder);
            ProjetoAfetado.Config(modelBuilder);

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            var servicos = new Solucao { CdSolucao = 1, NmSolucao = "ssc-serviços", DsSolucao = "Api privada e batches" };
            var modulos = new Solucao { CdSolucao = 2, NmSolucao = "ssc-modulos", DsSolucao = "Sistema interno da SEAC" };
            var canais = new Solucao { CdSolucao = 3, NmSolucao = "ssc-canais", DsSolucao = "Websites e apis públicas" };

            modelBuilder.Entity<Solucao>(solucao => solucao.HasData(servicos, modulos, canais));

            var projetos = new [] 
            {
                new { CdProjeto = 1, CD_SOLUCAO = 1, NmProjeto = "Api.Beneficios", DsProjeto = "Api Benefícios (Voucher)" },
                new { CdProjeto = 2, CD_SOLUCAO = 1, NmProjeto = "Api.Cartao", DsProjeto = "Api Cartão" },
                new { CdProjeto = 3, CD_SOLUCAO = 1, NmProjeto = "Api.Cliente", DsProjeto = "Api Cliente" },
                new { CdProjeto = 4, CD_SOLUCAO = 1, NmProjeto = "Api.Funcionario", DsProjeto = "Api Funcionário" },
                new { CdProjeto = 5, CD_SOLUCAO = 1, NmProjeto = "Api.Gerenciamento", DsProjeto = "Api Gerenciamento" },
                new { CdProjeto = 6, CD_SOLUCAO = 1, NmProjeto = "Api.Lojista", DsProjeto = "Api Lojista" },
                new { CdProjeto = 7, CD_SOLUCAO = 1, NmProjeto = "Api.Movimento", DsProjeto = "Api Movimento" },
                new { CdProjeto = 8, CD_SOLUCAO = 1, NmProjeto = "Api.Seguranca", DsProjeto = "Api Seguranca" },

                new { CdProjeto = 9, CD_SOLUCAO = 2, NmProjeto = "SSC.Cliente", DsProjeto = "Módulo Cliente" },
                new { CdProjeto = 10, CD_SOLUCAO = 2, NmProjeto = "SSC.Lojista", DsProjeto = "Módulo Lojista" },
                new { CdProjeto = 11, CD_SOLUCAO = 2, NmProjeto = "SSC.Gerenciamento", DsProjeto = "Módulo Gerenciamento" },

                new { CdProjeto = 12, CD_SOLUCAO = 3, NmProjeto = "PortalCliente", DsProjeto = "Portal Cliente" },
                new { CdProjeto = 13, CD_SOLUCAO = 3, NmProjeto = "PortalLojista", DsProjeto = "Portal Lojista" },
                new { CdProjeto = 14, CD_SOLUCAO = 3, NmProjeto = "PortalAssistente", DsProjeto = "Api pública Assistente" },
                new { CdProjeto = 15, CD_SOLUCAO = 3, NmProjeto = "PortalBeneficios", DsProjeto = "Api pública Benefícios" }
            };

            modelBuilder.Entity<Projeto>(projeto => projeto.HasData(projetos));
        }
    }
}
