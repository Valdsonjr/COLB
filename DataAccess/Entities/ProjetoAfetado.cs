using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Entities
{
    public class ProjetoAfetado
    {
        public Int32 CdProjeto { get; set; }
        public Projeto Projeto { get; set; }

        public Int64 NrOrdemDeLiberacao { get; set; }
        public OrdemDeLiberacao OrdemDeLiberacao { get; set; }

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjetoAfetado>(projetoAfetado =>
            {

                projetoAfetado.HasOne(pa => pa.Projeto)
                              .WithMany(p => p.ProjetosAfetados)
                              .HasForeignKey(pa => pa.CdProjeto)
                              .IsRequired();

                projetoAfetado.Property<Int32>(pa => pa.CdProjeto)
                              .IsRequired()
                              .HasColumnName("CD_PROJETO");

                projetoAfetado.Property<Int64>(pa => pa.NrOrdemDeLiberacao)
                              .IsRequired()
                              .HasColumnName("NR_ORDEM_DE_LIBERACAO");

                projetoAfetado.HasOne(pa => pa.OrdemDeLiberacao)
                              .WithMany(p => p.ProjetosAfetados)
                              .HasForeignKey(pa => pa.NrOrdemDeLiberacao)
                              .IsRequired();

                projetoAfetado.ToTable("TB_PROJETO_AFETADO").HasKey(pa => new { pa.CdProjeto, pa.NrOrdemDeLiberacao });
            });
        }
    }
}
