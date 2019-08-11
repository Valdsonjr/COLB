using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Entities
{
    public class Usuario
    {
        public Int32 CdUsuario { get; set; }
        public String NmUsuario { get; set; }
        public Int64 NrTelefone { get; set; }
        public String DsEmail { get; set; }
        public DateTime DtCadastro { get; set; }
        public Byte[] DsSenha { get; set; }
        public DateTime DtNascimento { get; set; }
        public Boolean FlAtivo { get; set; }
        public Perfil Perfil { get; set; }

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(funcionario =>
            {
                funcionario.ToTable("TB_USUARIO")
                           .HasKey(f => f.CdUsuario);

                funcionario.Property(f => f.CdUsuario)
                           .HasColumnName("CD_USUARIO")
                           .ValueGeneratedOnAdd()
                           .IsRequired();

                funcionario.Property(f => f.NmUsuario)
                           .HasColumnName("NM_USUARIO")
                           .IsRequired();

                funcionario.Property(f => f.NrTelefone)
                           .HasColumnName("NR_TELEFONE")
                           .IsRequired();

                funcionario.HasIndex(f => f.NrTelefone)
                           .IsUnique();

                funcionario.Property(f => f.DsEmail)
                           .HasColumnName("DS_EMAIL")
                           .IsRequired();

                funcionario.HasIndex(f => f.DsEmail)
                           .IsUnique();

                funcionario.Property(f => f.DtCadastro)
                           .HasColumnName("DT_CADASTRO")
                           .IsRequired();

                funcionario.Property(f => f.DtNascimento)
                           .HasColumnName("DT_NASCIMENTO")
                           .IsRequired();

                funcionario.Property(f => f.DsSenha)
                           .HasColumnName("DS_SENHA")
                           .IsRequired();

                funcionario.Property(f => f.FlAtivo)
                           .HasColumnName("FL_ATIVO")
                           .IsRequired();

                funcionario.HasOne(f => f.Perfil)
                           .WithMany(p => p.Usuarios)
                           .HasForeignKey("CD_PERFIL")
                           .IsRequired();
            });
        }
    }
}
