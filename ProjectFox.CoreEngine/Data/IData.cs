namespace ProjectFox.CoreEngine.Data;

/// <summary></summary>
/// <typeparam name="T"> inheriting type </typeparam>
public interface IData<T>
{
    public abstract static string ConcatHex(bool littleEndian, bool leadingText, params T[] values);

    public abstract static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params T[] values);

    public abstract static T FromBytes(byte[] bytes, bool littleEndian);

    public abstract static T[] FromBytesMultiple(byte[] bytes, bool littleEndian);//rename?

    public abstract static byte[] GetBytes(T[] values, bool littleEndian);

    public abstract static byte[][] GetBytesSeparate(T[] values, bool littleEndian);

    public abstract static string JoinHex(bool littleEndian, bool leadingText, string separator, params T[] values);

    public abstract static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params T[] values);

    public abstract byte[] GetBytes(bool littleEndian);

    public abstract string ToHexString(bool littleEndian = false, bool leadingText = false);

    public abstract string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_');
}
