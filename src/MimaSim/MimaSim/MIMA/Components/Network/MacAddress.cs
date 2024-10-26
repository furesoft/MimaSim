using System;
using System.Linq;

namespace MimaSim.MIMA.Components.Network;

public readonly record struct MacAddress(byte[] address)
{
    private readonly byte[] _address = address;

    public static MacAddress Parse(string macAddress)
    {
        if (!TryParse(macAddress, out var a))
            throw new FormatException("Ungültiges MAC-Adressformat.");

        return a;
    }

    public static MacAddress Generate()
    {
        var random = new Random();
        var bytes = new byte[6];
        random.NextBytes(bytes);

        bytes[0] = (byte)(bytes[0] & 0xFE);

        return new MacAddress(bytes);
    }

    public static bool TryParse(string macAddress, out MacAddress address)
    {
        var cleanedMac = macAddress.Replace(":", "").Replace("-", "").Replace(".", "");

        if (cleanedMac.Length != 12 || !cleanedMac.All(Uri.IsHexDigit))
        {
            address = default;
            return false;
        }

        address = new MacAddress(Enumerable.Range(0, 6)
            .Select(i => Convert.ToByte(cleanedMac.Substring(i * 2, 2), 16))
            .ToArray());

        return true;
    }

    public override string ToString()
    {
        return string.Join(":", _address.Select(b => b.ToString("X2")));
    }

    public byte[] GetBytes() => _address;
}