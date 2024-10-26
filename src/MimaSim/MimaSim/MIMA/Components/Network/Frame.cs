using System.Net;

namespace MimaSim.MIMA.Components.Network;

public readonly record struct Frame(IPAddress IPAddress, MacAddress MacAddress, byte[] Data)
{

}