using Services.Models;
using Services.Util;
using System;

namespace Services.Abstract
{
    /// <summary>
    /// Interface para serviços de usuário
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Autentica um usuário
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        GetUsuarioModel Autenticar(Email email, Password senha);

        /// <summary>
        /// Obtém as informações do usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GetUsuarioModel Obter(Int32 id);

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        GetUsuarioModel Cadastrar(PostUsuarioModel model);
    }
}
