using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Requisicao
    {
        public Int64 NrRequisicao { get; set; }
        public String DsRequisicao { get; set; }
        public DateTime DtSolicitacao { get; set; }
        public String NmSolicitante { get; set; }

        public virtual ICollection<OrdemDeLiberacao> OrdensDeLiberacao { get; set; }

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Requisicao>(requisicao =>
            {
                requisicao.ToTable("TB_REQUISICAO")
                          .HasKey(r => r.NrRequisicao);

                requisicao.Property(r => r.NrRequisicao)
                          .HasColumnName("NR_REQUISICAO")
                          .ValueGeneratedNever()
                          .IsRequired();

                requisicao.Property(r => r.DsRequisicao)
                          .HasColumnName("DS_REQUISICAO")
                          .IsRequired();

                requisicao.HasMany(r => r.OrdensDeLiberacao)
                          .WithOne(ol => ol.Requisicao)
                          .HasForeignKey("NR_REQUISICAO");

                requisicao.Property(r => r.DtSolicitacao)
                          .HasColumnName("DT_SOLICITACAO")
                          .IsRequired();

                requisicao.Property(r => r.NmSolicitante)
                          .HasColumnName("NM_SOLICITANTE")
                          .IsRequired();
            });
        }
    }
}
