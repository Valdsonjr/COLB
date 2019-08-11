using Microsoft.EntityFrameworkCore;
using System;

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
                projetoAfetado.Property<Int32>("CD_PROJETO")
                              .IsRequired();

                projetoAfetado.Property<Int64>("NR_ORDEM_DE_LIBERACAO")
                              .IsRequired();

                projetoAfetado.HasOne(pa => pa.OrdemDeLiberacao)
                              .WithMany(ol => ol.ProjetosAfetados)
                              .HasForeignKey("NR_ORDEM_DE_LIBERACAO")
                              .IsRequired();

                projetoAfetado.HasOne(pa => pa.Projeto)
                              .WithMany(p => p.ProjetosAfetados)
                              .HasForeignKey("CD_PROJETO")
                              .IsRequired();

                projetoAfetado.ToTable("TB_PROJETO_AFETADO").HasKey("CD_PROJETO", "NR_ORDEM_DE_LIBERACAO");
            });
        }
    }
}
