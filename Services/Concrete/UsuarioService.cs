using AutoMapper;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Abstract;
using Services.Models;
using Services.Util;
using System;
using System.Linq;
using System.Net;

namespace Services.Concrete
{
    /// <summary>
    /// Serviços relacionados ao usuário
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly Context context;
        private readonly IMapper mapper;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public UsuarioService(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        GetUsuarioModel IUsuarioService.Autenticar(Email email, Password senha)
        {
            var usuario = context.Usuarios.Include(u => u.Perfil).SingleOrDefault(f => f.DsEmail == email.Raw);

            if (usuario == null || !usuario.DsSenha.SequenceEqual(senha.Raw))
                throw new ApiException(HttpStatusCode.Unauthorized, "Usuário ou Senha inválidos.");

            return mapper.Map<Usuario, GetUsuarioModel>(usuario);
        }

        GetUsuarioModel IUsuarioService.Cadastrar(PostUsuarioModel model)
        {
            if (context.Usuarios.SingleOrDefault(f => f.DsEmail == model.DsEmail.Raw || f.NrTelefone == model.NrTelefone) != null)
                throw new ApiException(HttpStatusCode.Conflict, "Usuário já cadastrado.");

            if (!model.DsEmail.IsValid())
                throw new ApiException(HttpStatusCode.Conflict, "Email inválido.");

            var usuario = new Usuario
            {
                CdUsuario = 0, // ignorado
                DsSenha = model.DsSenha.Raw,
                DtCadastro = DateTime.Now,
                FlAtivo = true,
                NmUsuario = model.NmUsuario,
                DsEmail = model.DsEmail.Raw,
                NrTelefone = model.NrTelefone,
                DtNascimento = model.DtNascimento,
                Perfil = context.Perfis.Find(2), // Perfil de Funcionário
            };

            context.Add<Usuario>(usuario);
            context.SaveChanges();

            return mapper.Map<Usuario, GetUsuarioModel>(usuario);
        }

        GetUsuarioModel IUsuarioService.Obter(int id)
        {
            return mapper.Map<Usuario, GetUsuarioModel>(context.Usuarios
                                                               .Include(u => u.Perfil)
                                                               .SingleOrDefault(f => f.CdUsuario == id && f.FlAtivo));
        }
    }
}
