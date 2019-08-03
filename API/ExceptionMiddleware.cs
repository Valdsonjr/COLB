using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
    public class ExceptionMiddleware
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
                if (!String.IsNullOrWhiteSpace(ex.LogInfo))
                    logger.LogWarning(ex.LogInfo);

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
            context.Response.ContentType = context.Request.Headers["Accept"];
            context.Response.StatusCode = (Int32)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(
                new List<String> { "Ocorreu um erro inesperado, por favor entre em contato com o administrador do sistema" }.ToString()
            );
        }

        private static Task HandleExceptionAsync(HttpContext context, ApiException exception)
        {
            context.Response.ContentType = context.Request.Headers["Accept"];
            context.Response.StatusCode = (Int32)exception.StatusCode;

            return context.Response.WriteAsync(exception.ValidationErrors.ToString());
        }
    }
}
