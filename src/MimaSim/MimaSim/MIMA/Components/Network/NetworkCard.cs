using System;
using System.Net;
using MimaSim.Core;

namespace MimaSim.MIMA.Components.Network;

public class NetworkCard
{
    public IPAddress IP { get; set; }
    public MacAddress MAC { get; set; }

    public NetworkCard(ICache cache)
    {
        var mac = cache.Get<string>("config.nic.mac");
        if (mac == null)
        {
            MAC = MacAddress.Generate();
            cache.AddOrUpdate("config.nic.mac", MAC.ToString());
        }
        else
        {
            MAC = MacAddress.Parse(mac);
        }

        IP = GenerateRandomPublicIP();
    }

    public static IPAddress GenerateRandomPublicIP()
    {
        int firstOctet;

        do
        {
            firstOctet = Random.Shared.Next(1, 224);
        } while (firstOctet is >= 10 and <= 126); // exclude private IP-Ranges

        var secondOctet = Random.Shared.Next(0, 256);
        var thirdOctet = Random.Shared.Next(0, 256);
        var fourthOctet = Random.Shared.Next(0, 256);

        return IPAddress.Parse($"{firstOctet}.{secondOctet}.{thirdOctet}.{fourthOctet}");
    }
}