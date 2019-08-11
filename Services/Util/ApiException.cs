using System;
using System.Collections.Generic;
using System.Net;

namespace Services.Util
{
    /// <summary>
    /// Exceção customizada para uso interno da api
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Status Code
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Lista de erros de validação a ser mostrada para o usuário
        /// </summary>
        public List<String> ValidationErrors { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="statusCode"></param>
        public ApiException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            ValidationErrors = new List<String>();
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="validationErrors"></param>
        public ApiException(HttpStatusCode statusCode, List<String> validationErrors)
        {
            StatusCode = statusCode;
            ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="validationError"></param>
        public ApiException(HttpStatusCode statusCode, String validationError)
        {
            StatusCode = statusCode;
            ValidationErrors = new List<String> { validationError };
        }

        /// <summary>
        /// Adiciona um erro na lista de erros de validação
        /// </summary>
        /// <param name="error"></param>
        public void AddError(String error)
        {
            ValidationErrors.Add(error);
        }
    }
}
