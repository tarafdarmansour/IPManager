using System.Net;
using IPManager.Services;

namespace IPManager.Middleware;

public class IpCheckMiddleware
{
    private readonly IIPCheckService _blockingService;
    private readonly RequestDelegate _next;

    public IpCheckMiddleware(RequestDelegate next, IIPCheckService blockingService)
    {
        _next = next;
        _blockingService = blockingService;
    }

    public async Task Invoke(HttpContext context)
    {
        var remoteIp = context.Connection.RemoteIpAddress;
        var isBlocked = _blockingService.IsBlocked(remoteIp!);
        if (isBlocked)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return;
        }

        await _next.Invoke(context);
    }
}