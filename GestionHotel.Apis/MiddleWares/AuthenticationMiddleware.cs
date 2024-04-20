using DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Services;
using Microsoft.AspNetCore.Identity;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context, IUserService userService)
    {
        string? username = context.Request.Headers.Authorization;
        if(username.StartsWith("Bearer "))
        {
            username = username.Substring("Bearer ".Length);
        }
        bool isUserInDatabase = await userService.IsUserInDatabase(username);

        if (isUserInDatabase)
        {
            await _next(context);
        }
        context.Response.StatusCode = 403;
    }
}

public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuthenticationMiddleware>();
    }
}
