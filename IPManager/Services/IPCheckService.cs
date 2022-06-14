using System.Net;

namespace IPManager.Services;

public class IPCheckService : IIPCheckService
{
    private readonly List<string> _blackIps;
    private readonly List<string> _whiteIps;
    private readonly bool checkBlackList;
    private readonly bool checkwhiteList;

    public IPCheckService(IConfiguration configuration)
    {
        checkBlackList = configuration.GetValue<bool>("BlackListCheck");
        checkwhiteList = configuration.GetValue<bool>("WhiteListCheck");

        if (checkwhiteList)
        {
            var whiteList = configuration.GetValue<string>("WhiteList");
            _whiteIps = whiteList.Split(',').ToList();
        }

        if (checkBlackList)
        {
            var blackList = configuration.GetValue<string>("BlackList");
            _blackIps = blackList.Split(',').ToList();
        }
    }

    public bool IsBlocked(IPAddress ipAddress)
    {
        if (checkBlackList && _blackIps.Contains(ipAddress.ToString()))
            return true;

        if (checkwhiteList && !_whiteIps.Contains(ipAddress.ToString()))
            return true;

        return false;
    }
}