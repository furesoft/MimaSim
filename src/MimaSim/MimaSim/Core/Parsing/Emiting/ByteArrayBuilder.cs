using System;
using System.IO;
using System.Text;

namespace MimaSim.Core.Parsing.Emiting;

public class ByteArrayBuilder : IDisposable
{
    private const byte streamFalse = 0;

    private const byte streamTrue = 1;

    private MemoryStream _store = new();

    public ByteArrayBuilder()
    {
    }

    public ByteArrayBuilder(byte[] data)
    {
        _store.Close();
        _store.Dispose();
        _store = new MemoryStream(data);
    }

    public ByteArrayBuilder(string base64)
    {
        _store.Close();
        _store.Dispose();
        _store = new MemoryStream(Convert.FromBase64String(base64));
    }

    public int Length => (int)_store.Length;

    public void Dispose()
    {
        _store.Close();
        _store.Dispose();
    }

    public void Append(bool b)
    {
        _store.WriteByte(b ? streamTrue : streamFalse);
    }

    public void Append(byte b)
    {
        _store.WriteByte(b);
    }

    public void Append(byte[] b, bool addLength = true)
    {
        if (addLength) Append(b.Length);
        AddBytes(b);
    }

    public void Append(char c)
    {
        _store.WriteByte((byte)c);
    }

    public void Append(char[] c, bool addLength = true)
    {
        if (addLength) Append(c.Length);
        Append(Encoding.Unicode.GetBytes(c));
    }

    public void Append(DateTime dt)
    {
        Append(dt.Ticks);
    }

    public void Append(decimal d)
    {
        var bits = decimal.GetBits(d);
        Append(bits[0]);
        Append(bits[1]);
        Append(bits[2]);
        Append(bits[3]);
    }

    public void Append(double d)
    {
        AddBytes(BitConverter.GetBytes(d));
    }

    public void Append(float f)
    {
        AddBytes(BitConverter.GetBytes(f));
    }

    public void Append(Guid g)
    {
        Append(g.ToByteArray());
    }

    public void Append(int i)
    {
        AddBytes(BitConverter.GetBytes(i));
    }

    public void Append(long l)
    {
        AddBytes(BitConverter.GetBytes(l));
    }

    public void Append(short i)
    {
        AddBytes(BitConverter.GetBytes(i));
    }

    public void Append(string s, bool addLength = true)
    {
        var data = Encoding.Unicode.GetBytes(s);
        if (addLength) Append(data.Length);
        AddBytes(data);
    }

    public void Append(uint ui)
    {
        AddBytes(BitConverter.GetBytes(ui));
    }

    public void Append(ulong ul)
    {
        AddBytes(BitConverter.GetBytes(ul));
    }

    public void Append(ushort us)
    {
        AddBytes(BitConverter.GetBytes(us));
    }

    public void Clear()
    {
        _store.Close();
        _store.Dispose();
        _store = new MemoryStream();
    }

    public void ReplaceAt(int index, byte value)
    {
        ValidateIndex(index);
        var buffer = _store.GetBuffer();
        buffer[index] = value;
    }

    public void ReplaceAt(int index, bool value)
    {
        ValidateIndex(index);
        ReplaceAt(index, value ? streamTrue : streamFalse);
    }

    public void ReplaceAt(int index, char value)
    {
        ValidateIndex(index);
        ReplaceAt(index, (byte)value);
    }

    public void ReplaceAt(int index, short value)
    {
        ValidateIndex(index);
        var bytes = BitConverter.GetBytes(value);
        Array.Copy(bytes, 0, _store.GetBuffer(), index, bytes.Length);
    }

    public void ReplaceAt(int index, int value)
    {
        ValidateIndex(index);
        var bytes = BitConverter.GetBytes(value);
        Array.Copy(bytes, 0, _store.GetBuffer(), index, bytes.Length);
    }

    public void ReplaceAt(int index, long value)
    {
        ValidateIndex(index);
        var bytes = BitConverter.GetBytes(value);
        Array.Copy(bytes, 0, _store.GetBuffer(), index, bytes.Length);
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= Length)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
    }

    public bool GetBool()
    {
        return _store.ReadByte() == streamTrue;
    }

    public byte GetByte()
    {
        return (byte)_store.ReadByte();
    }

    public byte[] GetByteArray()
    {
        var length = GetInt();
        return GetBytes(length);
    }

    public char GetChar()
    {
        return (char)_store.ReadByte();
    }

    public char[] GetCharArray()
    {
        var length = GetInt();
        return Encoding.Unicode.GetChars(GetBytes(length));
    }

    public DateTime GetDateTime()
    {
        return new DateTime(GetLong());
    }

    public decimal GetDecimal()
    {
        int[] bits = [GetInt(), GetInt(), GetInt(), GetInt()];
        return new decimal(bits);
    }

    public double GetDouble()
    {
        return BitConverter.ToDouble(GetBytes(8), 0);
    }

    public float GetFloat()
    {
        return BitConverter.ToSingle(GetBytes(4), 0);
    }

    public Guid GetGuid()
    {
        return new Guid(GetByteArray());
    }

    public int GetInt()
    {
        return BitConverter.ToInt32(GetBytes(4), 0);
    }

    public long GetLong()
    {
        return BitConverter.ToInt64(GetBytes(8), 0);
    }

    public short GetShort()
    {
        return BitConverter.ToInt16(GetBytes(2), 0);
    }

    public string GetString()
    {
        var length = GetInt();
        return Encoding.Unicode.GetString(GetBytes(length));
    }

    public uint GetUint()
    {
        return BitConverter.ToUInt32(GetBytes(4), 0);
    }

    public ulong GetUlong()
    {
        return BitConverter.ToUInt64(GetBytes(8), 0);
    }

    public ushort GetUshort()
    {
        return BitConverter.ToUInt16(GetBytes(2), 0);
    }

    public void Rewind()
    {
        _store.Seek(0, SeekOrigin.Begin);
    }

    public void Seek(int position)
    {
        _store.Seek(position, SeekOrigin.Begin);
    }

    public byte[] ToArray()
    {
        var data = new byte[Length];
        Array.Copy(_store.GetBuffer(), data, Length);
        return data;
    }

    public override string ToString()
    {
        return Convert.ToBase64String(ToArray());
    }

    private void AddBytes(byte[] b)
    {
        _store.Write(b, 0, b.Length);
    }

    private byte[] GetBytes(int length)
    {
        var data = new byte[length];
        if (length > 0)
        {
            var read = _store.Read(data, 0, length);
            if (read != length) throw new ApplicationException("Buffer did not contain " + length + " bytes");
        }

        return data;
    }
}