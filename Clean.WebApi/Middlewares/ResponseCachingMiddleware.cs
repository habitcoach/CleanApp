using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ResponseCachingMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseCachingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.GetTypedHeaders().CacheControl =
            new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            {
                Public = true,// public cache are shared by different user
                MaxAge = TimeSpan.FromSeconds(30)
            };

        context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
            new string[] { "Accept-Encoding" };

        await _next(context);
    }
}
