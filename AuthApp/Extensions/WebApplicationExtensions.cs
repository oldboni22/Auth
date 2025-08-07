using System.Diagnostics;
using Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace AuthApp.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureExceptionHandling(this WebApplication application)
    {
        application.UseExceptionHandler(builder => 
            builder.Run
            (
                async context =>
                {
                    var handlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (handlerFeature == null)
                        return;

                    context.Response.StatusCode = handlerFeature.Error switch
                    {
                        IncorrectUserLoginCredentialsException => StatusCodes.Status401Unauthorized,
                        UserAlreadyExistsException => StatusCodes.Status409Conflict,
                        UserIdDoesNotExist => StatusCodes.Status404NotFound,
                        ArgumentException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    var details = new ErrorDetails(context.Response.StatusCode, handlerFeature.Error.Message);

                    await context.Response.WriteAsync(details.ToString());
                })
            );
    }
}