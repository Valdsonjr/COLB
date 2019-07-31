using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Projeto
    {
        public Int32 CdProjeto { get; set; }
        public String NmProjeto { get; set; }
        public String DsProjeto { get; set; }
        public Solucao Solucao { get; set; }

        public virtual ICollection<ProjetoAfetado> ProjetosAfetados { get; set; }

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projeto>(projeto =>
            {
                projeto.HasOne(p => p.Solucao)
                       .WithMany(s => s.Projetos)
                       .HasForeignKey("CD_SOLUCAO")
                       .IsRequired();

                projeto.ToTable("TB_PROJETO")
                       .HasKey(p => p.CdProjeto);

                projeto.Property(p => p.CdProjeto)
                       .HasColumnName("CD_PROJETO")
                       .ValueGeneratedOnAdd()
                       .IsRequired();

                projeto.Property(p => p.NmProjeto)
                       .HasColumnName("NM_PROJETO")
                       .IsRequired();

                projeto.Property(p => p.DsProjeto)
                       .HasColumnName("DS_PROJETO")
                       .IsRequired();
            });
        }
    }
}
