using Serilog;
using System.Diagnostics;
using System.Text;

namespace Rommanel.API.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        var request = context.Request;
        var method = request.Method;
        var path = request.Path;

        var requestBody = "";
        if (method == HttpMethods.Post || method == HttpMethods.Put)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
        }

        Log.Information("HTTP {Method} {Path} started. Body: {Body}", method, path, requestBody);

        await _next(context);

        stopwatch.Stop();
        Log.Information("HTTP {Method} {Path} finished in {ElapsedMilliseconds}ms with status {StatusCode}",
            method, path, stopwatch.ElapsedMilliseconds, context.Response.StatusCode);
    }
}
