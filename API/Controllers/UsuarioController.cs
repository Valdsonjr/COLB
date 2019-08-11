using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstract;
using Services.Models;
using Services.Util;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    /// <summary>
    /// Endpoints relacionados ao usuário
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;
        private readonly AppSettings options;
        private readonly IMapper mapper;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="usuarioService"></param>
        /// <param name="options"></param>
        /// <param name="mapper"></param>
        public UsuarioController(IUsuarioService usuarioService, IOptions<AppSettings> options, IMapper mapper)
        {
            this.usuarioService = usuarioService;
            this.options = options.Value;
            this.mapper = mapper;
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(GetUsuarioModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status409Conflict)]
        public IActionResult Post([FromBody] PostUsuarioModel model)
        {
            var usuario = usuarioService.Cadastrar(model);
            return CreatedAtAction("GetById", new { id = usuario.CdUsuario }, usuario);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Sessao")]
        [ProducesResponseType(typeof(GetSessaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
        public IActionResult PostSessao([FromBody] PostSessaoModel model)
        {
            var usuario = usuarioService.Autenticar(model.Email, model.Senha);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim("Id", usuario.CdUsuario.ToString()),
                    new Claim("Perfil", usuario.Perfil.CdPerfil.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new GetSessaoModel
            {
                Id = usuario.CdUsuario,
                Nome = usuario.NmUsuario,
                Token = tokenString,
                Perfil = usuario.Perfil.DsPerfil
            });
        }

        /// <summary>
        /// Obtém as informações do usuário logado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetUsuarioModel), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = Int32.Parse(identity.FindFirst("Id").Value);

            var funcionario = usuarioService.Obter(id);
            return Ok(funcionario);
        }

        /// <summary>
        /// Obtém as informações de um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetOutroUsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var funcionario = usuarioService.Obter(id);

            if (funcionario == null)
                throw new ApiException(HttpStatusCode.NotFound, "Usuário não encontrado.");

            return Ok(mapper.Map<GetUsuarioModel, GetOutroUsuarioModel>(funcionario));
        }
    }
}