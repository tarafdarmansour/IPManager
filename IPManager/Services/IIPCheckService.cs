using System.Net;

namespace IPManager.Services;

public interface IIPCheckService
{
    bool IsBlocked(IPAddress ipAddress);
}