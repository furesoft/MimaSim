using System;
using System.Linq;
using System.Net;
using MimaSim.Core;
using Splat;

namespace MimaSim.MIMA.Components.Network;

public class NetworkCard
{
    public IPAddress IP { get; set; }
    public IPAddress SubnetMask { get; set; }
    public IPAddress NetworkAddress { get; set; }
    public MacAddress MAC { get; set; }

    private INetworkService _networkService;

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

        var subnetMask = cache.Get<string>("config.nic.subnetmask");
        if (subnetMask == null)
        {
            SubnetMask = IPAddress.Parse("255.255.255.0");
        }
        else
        {
            SubnetMask = IPAddress.Parse(subnetMask);
        }

        IP = GenerateRandomPublicIP();
        NetworkAddress = new IPAddress(CaclulateNetworkAddress(IP, SubnetMask.GetAddressBytes()));

        _networkService = Locator.Current.GetService<INetworkService>()!;
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

    public bool IsInSameSubnet(IPAddress? ipAddress)
    {
        if (ipAddress == null)
            throw new ArgumentNullException(nameof(ipAddress));

        // calculate network address
        var ipBytes = ipAddress.GetAddressBytes();
        var subnetMaskBytes = SubnetMask.GetAddressBytes();
        var networkAddressBytes = new byte[ipBytes.Length];

        for (int i = 0; i < ipBytes.Length; i++)
        {
            networkAddressBytes[i] = (byte)(ipBytes[i] & subnetMaskBytes[i]);
        }

        // calculate network address for own ip address
        var ownNetworkAddressBytes = CaclulateNetworkAddress(IP, subnetMaskBytes);

        return networkAddressBytes.SequenceEqual(ownNetworkAddressBytes);
    }

    public static byte[] CaclulateNetworkAddress(IPAddress ip, byte[] subnetMaskBytes)
    {
        var ownIpBytes = ip.GetAddressBytes();
        var ownNetworkAddressBytes = new byte[ownIpBytes.Length];

        for (int i = 0; i < ownIpBytes.Length; i++)
        {
            ownNetworkAddressBytes[i] = (byte)(ownIpBytes[i] & subnetMaskBytes[i]);
        }

        return ownNetworkAddressBytes;
    }

    public void Send(IPAddress? ipAddress, byte[] data)
    {
        if (IsInSameSubnet(ipAddress))
        {
            var frame = new Frame(IP, MAC, ipAddress, default, data);
            _networkService.Send(frame);
        }
    }

    public void Send(MacAddress macAddress, byte[] data)
    {
        var frame = new Frame(IP, MAC, default, macAddress, data);
        _networkService.Send(frame);
    }

    public void Loopback(byte[] data)
    {
        var frame = new Frame(IP, MAC, IP, MAC, data);
        _networkService.Send(frame);
    }
}