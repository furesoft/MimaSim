using System.Net;

namespace MimaSim.MIMA.Components.Network;

public readonly record struct Frame(IPAddress FromIPAddress, MacAddress FromMacAddress,
    IPAddress ToIPAddress, MacAddress ToMacAddress, byte[] Data);