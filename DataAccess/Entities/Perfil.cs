using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Perfil
    {
        public Int32 CdPerfil { get; set; }
        public String DsPerfil { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Perfil>(perfil =>
            {
                perfil.ToTable("TB_PERFIL")
                      .HasKey(p => p.CdPerfil);

                perfil.Property(p => p.CdPerfil)
                      .ValueGeneratedOnAdd()
                      .HasColumnName("CD_PERFIL")
                      .IsRequired();

                perfil.Property(p => p.DsPerfil)
                      .HasColumnName("DS_PERFIL")
                      .IsRequired();

                perfil.HasMany(p => p.Usuarios)
                      .WithOne(f => f.Perfil)
                      .HasForeignKey("CD_PERFIL")
                      .IsRequired();
            });
        }
    }
}
