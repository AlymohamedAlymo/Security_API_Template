﻿using Security_API_Template.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace Security_API_Template.Middleware
{
    public class ExceptionMiddleware(RequestDelegate request, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = environment.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
                    : new ApiException(context.Response.StatusCode, "هناك خطاء", "حدث خطاء ما في الخدمة");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

    }
}