using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Solucao
    {
        public Int32 CdSolucao { get; set; }
        public String NmSolucao { get; set; }
        public String DsSolucao { get; set; }

        public virtual ICollection<Projeto> Projetos { get; set; }

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Solucao>(solucao => 
            {
                solucao.HasMany(s => s.Projetos)
                       .WithOne(p => p.Solucao)
                       .HasForeignKey("CD_SOLUCAO")
                       .IsRequired();

                solucao.ToTable("TB_SOLUCAO")
                       .HasKey(s => s.CdSolucao);

                solucao.Property(s => s.CdSolucao)
                       .HasColumnName("CD_SOLUCAO")
                       .ValueGeneratedOnAdd()
                       .IsRequired();

                solucao.Property(s => s.DsSolucao)
                       .HasColumnName("DS_SOLUCAO")
                       .IsRequired();

                solucao.Property(s => s.NmSolucao)
                       .HasColumnName("NM_SOLUCAO")
                       .IsRequired();
            });
        }
    }
}
