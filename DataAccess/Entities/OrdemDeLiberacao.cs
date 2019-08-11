using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class OrdemDeLiberacao
    {
        public Int64 NrOrdemDeLiberacao { get; set; }
        public Requisicao Requisicao { get; set; }
        public virtual ICollection<ProjetoAfetado> ProjetosAfetados { get; set; }

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdemDeLiberacao>(ordemDeLiberacao =>
            {
                ordemDeLiberacao.ToTable("TB_ORDEM_DE_LIBERACAO")
                                .HasKey(ol => ol.NrOrdemDeLiberacao);

                ordemDeLiberacao.Property<Int64>("NR_REQUISICAO")
                                .IsRequired();

                ordemDeLiberacao.HasOne(ol => ol.Requisicao)
                                .WithMany(r => r.OrdensDeLiberacao)
                                .HasForeignKey("NR_REQUISICAO")
                                .IsRequired();

                ordemDeLiberacao.Property(ol => ol.NrOrdemDeLiberacao)
                                .HasColumnName("NR_ORDEM_DE_LIBERACAO")
                                .ValueGeneratedNever()
                                .IsRequired();
            });
        }
    }
}
