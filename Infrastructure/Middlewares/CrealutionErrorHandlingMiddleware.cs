﻿using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CrealutionServer.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace CrealutionServer.Infrastructure.Middlewares
{
    public class CrealutionErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CrealutionErrorHandlingMiddleware(
            RequestDelegate next, 
            ILogger logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = "Internal Server Error";

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is CrealutionEntityNotFound)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                error = "Not found";
            }

            _logger.Error(
                exception, 
                error);

            var responce = new
            {
                error = error,
                message = exception.Message,
                code = context.Response.StatusCode,
                date = DateTime.Now
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(responce));
        }
    }
}