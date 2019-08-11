using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace API
{
    /// <summary>
    /// Middleware para tratamento de erros global
    /// </summary>
    internal class ExceptionMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="requestDelegate"></param>
        /// <param name="logger"></param>
        public ExceptionMiddleware(RequestDelegate requestDelegate, ILoggerFactory logger)
        {
            this.logger = logger.CreateLogger("ExceptionMiddleware");
            this.requestDelegate = requestDelegate;
        }

        /// <summary>
        /// Try-Catch Global
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);
            }
            catch (ApiException ex)
            {
                if (ex.ValidationErrors.Count > 0)
                    logger.LogWarning(String.Join('\n', ex.ValidationErrors));

                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                await HandleExceptionAsync(httpContext);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (Int32)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(
                new List<String> { "Ocorreu um erro inesperado, por favor entre em contato com o administrador do sistema" }));
        }

        private static Task HandleExceptionAsync(HttpContext context, ApiException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (Int32)exception.StatusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(exception.ValidationErrors));
        }
    }
}
