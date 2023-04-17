using Messager.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Messager.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException exception)
            {
                await HandleException(context, exception.Message, HttpStatusCode.NotFound);
            }
            catch (ApplicationException exception)
            {
                await HandleException(context, exception.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception exception)
            {
                await HandleException(context, "Some error. Try later.", HttpStatusCode.BadRequest);
            }
        }

        public async Task HandleException(HttpContext context, string message, HttpStatusCode httpStatusCode)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = @"text/plain";
            await context.Response.WriteAsync(message);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}