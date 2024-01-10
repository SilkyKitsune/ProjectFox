namespace ProjectFox.CoreEngine.Math;

/// <summary></summary>
/// <typeparam name="T"> inheriting type </typeparam>
public interface IData<T>
{
    public abstract static string ConcatHex(bool littleEndian, bool leadingText, params T[] values);//readonlyspan overload?

    public abstract static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params T[] values);//readonlyspan overload?

    public abstract static T FromBytes(byte[] bytes);//endianess?

    public abstract static T[] FromBytesMultiple(byte[] bytes);//rename?

    public abstract static byte[] GetBytes(T[] values);

    public abstract static string JoinHex(bool littleEndian, bool leadingText, string separator, params T[] values);//readonlyspan overload?

    public abstract static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params T[] values);//readonlyspan overload?

    public abstract byte[] GetBytes();

    public abstract string ToHexString(bool littleEndian = false, bool leadingText = false);

    public abstract string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_');
}
