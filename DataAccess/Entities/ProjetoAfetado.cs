using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities
{
    public class ProjetoAfetado
    {
        public Projeto Projeto { get; set; }
        public OrdemDeLiberacao OrdemDeLiberacao { get; set; }

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjetoAfetado>(projetoAfetado =>
            {

                projetoAfetado.HasOne(pa => pa.Projeto)
                              .WithMany(p => p.ProjetosAfetados)
                              .HasForeignKey("CD_PROJETO")
                              .IsRequired();

                projetoAfetado.HasOne(pa => pa.OrdemDeLiberacao)
                              .WithMany(p => p.ProjetosAfetados)
                              .HasForeignKey("NR_ORDEM_DE_LIBERACAO")
                              .IsRequired();

                projetoAfetado.ToTable("TB_PROJETO_AFETADO").HasKey("CD_PROJETO", "NR_ORDEM_DE_LIBERACAO");
            });
        }
    }
}
