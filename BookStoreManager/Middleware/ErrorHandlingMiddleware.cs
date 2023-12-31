﻿using BookStoreManager.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookStoreManager.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (IncorrectUserException incorrectUserException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(incorrectUserException.Message);
            }
            catch (ItemNotFoundException itemNotFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(itemNotFoundException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
