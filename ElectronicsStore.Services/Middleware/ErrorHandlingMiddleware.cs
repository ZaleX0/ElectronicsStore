using ElectronicsStore.Services.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ElectronicsStore.Services.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next.Invoke(context);
		}
		catch (BadRequestException e)
		{
			context.Response.StatusCode = 400;
			await context.Response.WriteAsync(e.Message);
		}
        catch (NotFoundException e)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(e.Message);
        }
        catch (Exception e)
		{
			Console.WriteLine(e.Message);

			context.Response.StatusCode = 500;
			await context.Response.WriteAsync("Something went wrong");
		}
    }
}
