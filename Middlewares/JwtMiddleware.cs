using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using StudentService.DTOs;
using StudentService.Middlewares.helpers.Response;
using StudentService.Middlewares.helpers.utils;
using System.Threading.Tasks;

namespace StudentService.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string? token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];
            if (token == null)
            {

                if (IsEnabledUnauthorizedRoute(httpContext))
                {
                    return _next(httpContext);
                }
            
            BaseResponse response = new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Unauthorized"));
            httpContext.Response.StatusCode = response.status_code;
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsJsonAsync(response);
            }
            else {
                if (JwtUtils.ValidateJwtToken(token))
                {
                    return _next(httpContext);
                }
                else
                {
                    BaseResponse response = new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Unauthorized"));
                    httpContext.Response.StatusCode = response.status_code;
                    httpContext.Response.ContentType = "application/json";
                    return httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }

        private bool IsEnabledUnauthorizedRoute(HttpContext httpContext)
        {
            List<string> enableRoutes = new List<string>
            {
                "/api/Student/save",
                "/api/Auth/authenticate",

            };
            bool isEnableUnauthoriedRoute = false;

            if(httpContext.Request.Path.Value is not null)
            {
                isEnableUnauthoriedRoute = enableRoutes.Contains(httpContext.Request.Path.Value);
            }
            return isEnableUnauthoriedRoute;
        }
    }


    

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
