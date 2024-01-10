using Contracts;
using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CodeMaze_V2.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app,ILoggerManager logger) // extension method to configure the global exception handler
        {
            // configure the exception handler middleware to log the exceptions to the console and return a response to the client
            // the response is in the form of a json payload with details of the exception
            // the status code for the response is 500 - Internal Server Error
            // the response will be returned as a json payload
            // the response will contain the following details: status code, message, and the exception
            app.UseExceptionHandler(appError =>
            { 
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                    context.Response.ContentType = "application/json"; // set the content type to json
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>(); // get the exception handler feature from the context feature collection 
                    if (contextFeature != null) // check if the context feature is not null
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails() // write the error details to the response
                        {
                            StatusCode = context.Response.StatusCode, 
                            Message = contextFeature.Error.Message, 
                        }.ToString());
                    }
                });
            });
        }

    }
}