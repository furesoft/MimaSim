using System.Net;

namespace MimaSim.MIMA.Components.Network;

public readonly record struct Frame(
    IPAddress FromIPAddress,
    MacAddress FromMacAddress,
    IPAddress? ToIPAddress,
    MacAddress ToMacAddress,
    byte[] Data)
{
    public byte[] Pack()
    {
        using (var memoryStream = new System.IO.MemoryStream())
        {
            // Pack FromIPAddress
            byte[] fromIpBytes = FromIPAddress.GetAddressBytes();
            memoryStream.Write(fromIpBytes, 0, fromIpBytes.Length);

            // Pack FromMacAddress
            byte[] fromMacBytes = FromMacAddress.GetBytes();
            memoryStream.Write(fromMacBytes, 0, fromMacBytes.Length);

            // Pack ToIPAddress
                byte[] toIpBytes = ToIPAddress.GetAddressBytes();
                memoryStream.Write(toIpBytes, 0, toIpBytes.Length);

            // Pack ToMacAddress
            byte[] toMacBytes = ToMacAddress.GetBytes();
            memoryStream.Write(toMacBytes, 0, toMacBytes.Length);

            // Pack Data
            memoryStream.Write(Data, 0, Data.Length);

            return memoryStream.ToArray();
        }
    }

    public static Frame Unpack(byte[] data)
    {
        using (var memoryStream = new System.IO.MemoryStream(data))
        {
            // Unpack FromIPAddress
            byte[] fromIpBytes = new byte[4]; // Assuming IPv4
            memoryStream.Read(fromIpBytes, 0, fromIpBytes.Length);
            IPAddress fromIp = new IPAddress(fromIpBytes);

            // Unpack FromMacAddress
            byte[] fromMacBytes = new byte[6]; // Assuming MAC address is 6 bytes
            memoryStream.Read(fromMacBytes, 0, fromMacBytes.Length);
            MacAddress fromMac = new MacAddress(fromMacBytes);

            // Unpack ToIPAddress

            byte[] toIpBytes = new byte[4];
            memoryStream.Read(toIpBytes, 0, toIpBytes.Length);
            IPAddress? toIp = new IPAddress(toIpBytes);

            // Unpack ToMacAddress
            byte[] toMacBytes = new byte[6];
            memoryStream.Read(toMacBytes, 0, toMacBytes.Length);
            MacAddress toMac = new MacAddress(toMacBytes);

            // Unpack Data
            byte[] dataBuffer = new byte[memoryStream.Length - memoryStream.Position];
            memoryStream.Read(dataBuffer, 0, dataBuffer.Length);

            return new Frame(fromIp, fromMac, toIp, toMac, dataBuffer);
        }
    }
}