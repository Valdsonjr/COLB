using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Abstract;
using Services.Concrete;
using Services.Util;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Tests
{
    [TestClass]
    public class UsuarioServiceTest
    {
        private static readonly DbContextOptions<Context> options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "UsuarioServiceTest").Options;
        private static Usuario usr;

        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            byte[] pwd;
            using (var sha = SHA512.Create())
            {
                pwd = sha.ComputeHash(Encoding.UTF8.GetBytes("abc123"));
            }

            using var context = new Context(options);
            usr = new Usuario
            {
                CdUsuario = 0, // ignorado
                NmUsuario = "Usr Teste",
                DtCadastro = DateTime.Now,
                FlAtivo = true,
                DsEmail = "teste01@email.com",
                NrTelefone = 1234567890,
                DsSenha = pwd,
                DtNascimento = DateTime.Now,
                Perfil = context.Perfis.Find(2)
            };

            context.Add<Usuario>(usr);

            context.SaveChanges();
        }

        [TestMethod]
        public void ObterTest()
        {
            using var context = new Context(options);
            IUsuarioService usuarioService = new UsuarioService(context, MapperProfile.GetMapper());
            var user = usuarioService.Obter(1);

            Assert.IsNotNull(user);
        }
    }
}
