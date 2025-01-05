using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Data;
using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine;

public class ResourceFile//rename?
{
    /*
     * --- Format Specification ---
     * 
     * Header "PRJCTfox" (ulong)
     * Size of file in bytes (uint)
     * 
     * Number of resource type sub-headers (ushort)
     * 
     * Resource type sub-headers (struct[])
     *   Resource type ID (ulong)
     *   Number of resources (ushort)
     *   Index of resource IDs (int)
     *   Address of resource data (int)
     * 
     * Resource IDs (ulong[])
     * 
     * Resource data (byte[])
     * 
     * 
     * --- Resource Data Specifications ---
     * 
     * WavShpe0 (WaveShape)
     *   Length (int)
     *   Whether sample data is mono, 0 = stereo, 1 = mono (byte)
     *   Samples (Sample[])
     *   
     * ClrTxtr0 (ColorTexture)
     *   Size (Vector)
     *     Width (int)
     *     Height (int)
     *   Whether pixels are grayscale, 0 = false, 1 = true (byte)
     *   Whether pixels use a single alpha, 0 = false, 1 = true (byte)
     *     Single alpha value, only present if previous byte is 1 (byte)
     *   Pixels (Color[])
     * 
     * PltTxtr0 (PalettizedTexture)
     *   Size (Vector)
     *     Width (int)
     *     Height (int)
     *   Bits per pixel, 1-8 (byte)
     *   Pixels (byte[])
     * 
     * ClrPlte0 (ColorPalette)
     *   Length (int)
     *   Whether pixels are grayscale, 0 = false, 1 = true (byte)
     *   Whether pixels use a single alpha, 0 = false, 1 = true (byte)
     *     Single alpha value, only present if previous byte is 1 (byte)
     *   Colors (Color[])
     * 
     * IdxPlte0 (IndexPalette)
     *   Length (int)
     *   Bits per index, 1-8 (byte)
     *   Indices (byte[])
     * 
    */
    
    protected delegate void PackDelegate(out NameID[] resourceIDs, out byte[] resourceData);
    protected delegate int UnpackDelegate(NameID[] resourceIDs, byte[] resourceData, int startAddress);

    private struct TypeSubHeader
    {
        internal const int Size = sizeof(ulong) + sizeof(ushort) + (sizeof(int) * 2);

        internal static TypeSubHeader[] FromBytes(byte[] bytes)
        {
            AutoSizedArray<TypeSubHeader> typeSubHeaders = new(/*?*/);
            for (int i = 0; i < bytes.Length;) typeSubHeaders.Add(new TypeSubHeader()
            {
                typeID = new((ulong)Data.ToInt64(bytes[i..(i += sizeof(ulong))], false)),
                resourceCount = (ushort)Data.ToInt16(bytes[i..(i += sizeof(ushort))], true),
                index = Data.ToInt32(bytes[i..(i += sizeof(int))], true),
                address = Data.ToInt32(bytes[i..(i += sizeof(int))], true)
            });
            return typeSubHeaders.ToArray();
        }

        internal static byte[] GetBytes(TypeSubHeader[] types)
        {
            AutoSizedArray<byte> bytes = new(/*?*/);
            foreach (TypeSubHeader type in types)
            {
                bytes.Add(Data.GetBytes((long)(ulong)type.typeID, false));
                bytes.Add(Data.GetBytes((short)type.resourceCount, true));
                bytes.Add(Data.GetBytes(type.index, true));
                bytes.Add(Data.GetBytes(type.address, true));
            }
            return bytes.ToArray();
        }

        internal NameID typeID;
        internal ushort resourceCount;
        internal int index, address;
    }

    protected sealed class ResourceDelegates
    {
        public NameID id;
        public PackDelegate pack;
        public UnpackDelegate unpack;
    }

    private const ulong
        //PRJCTfox              P  R  J  C  T  f  o  x
        Header =              0x50_52_4A_43_54_66_6F_78uL,
        //WavShpe0              W  a  v  S  h  p  e  0
        WaveShapeID =         0x57_61_76_53_68_70_65_00uL,
        //ClrTxtr0              C  l  r  T  x  t  r  0
        ColorTextureID =      0x43_6C_72_54_78_74_72_00uL,
        //PltTxtr0              P  l  t  T  x  t  r  0
        PalettizedTextureID = 0x50_6C_74_54_78_74_72_00uL,
        //ClrPlte0              C  l  r  P  l  t  e  0
        ColorPaletteID =      0x43_6C_72_50_6C_74_65_00uL,
        //IdxPlte0              I  d  x  P  l  t  e  0
        IndexPaletteID =      0x49_64_78_50_6C_74_65_00uL,
        //TxtrAnm0              T  x  t  r  A  n  m  0
        TextureAnimationID =  0x54_78_74_72_41_6E_6D_00uL,
        //PlteAnm0              P  l  t  e  A  n  m  0
        PaletteAnimationID =  0x50_6C_74_65_41_6E_6D_00uL;

    protected ResourceFile(params ResourceDelegates[] resourceDelegates) : this()
    {
        foreach (ResourceDelegates resourceDelegate in resourceDelegates)
            if (resourceDelegate != null && resourceDelegate.pack != null && resourceDelegate.unpack != null && !delegates.ContainsCode(resourceDelegate.id))
                delegates.Add(resourceDelegate.id, resourceDelegate);
    }

    public ResourceFile()
    {
        NameID waveShapeID = new(WaveShapeID), colorTextureID = new(ColorTextureID), palettizedTextureID = new(PalettizedTextureID), colorPaletteID = new(ColorPaletteID), indexPaletteID = new(IndexPaletteID);
        delegates.Add(waveShapeID, new()
        {
            id = waveShapeID,
            pack = PackWaveShapes,
            unpack = UnpackWaveShapes
        });
        delegates.Add(colorTextureID, new()
        {
            id = colorTextureID,
            pack = PackColorTextures,
            unpack = UnpackColorTextures
        });
        delegates.Add(palettizedTextureID, new()
        {
            id = palettizedTextureID,
            pack = PackPalettizedTextures,
            unpack = UnpackPalettizedTextures
        });
        delegates.Add(colorPaletteID, new()
        {
            id = colorPaletteID,
            pack = PackColorPalettes,
            unpack = UnpackColorPalettes
        });
        delegates.Add(indexPaletteID, new()
        {
            id = indexPaletteID,
            pack = PackIndexPalettes,
            unpack = UnpackIndexPalettes
        });
    }

    private readonly LookupTable<NameID, ResourceDelegates> delegates = new(8);

    public readonly LookupTable<NameID, ColorPalette> colorPalettes = new(0x20);
    public readonly LookupTable<NameID, ColorTexture> colorTextures = new(0x20);
    public readonly LookupTable<NameID, byte[]> indexPalettes = new(0x20);
    //public readonly LookupTable<NameID, PaletteAnimation> paletteAnimations = new(0x20);
    public readonly LookupTable<NameID, PalettizedTexture> palettizedTextures = new(0x20);
    //public readonly LookupTable<NameID, TextureAnimation> textureAnimations = new(0x20);
    public readonly LookupTable<NameID, Sample[]> waveShapes = new(0x20);

    private byte[] GetIDBytes(NameID[] ids)
    {
        AutoSizedArray<byte> bytes = new(/*?*/);
        foreach (NameID id in ids) bytes.Add(Data.GetBytes((long)(ulong)id, false));
        return bytes.ToArray();
    }

    private NameID[] GetIDs(byte[] bytes)
    {
        AutoSizedArray<NameID> ids = new(/*?*/);
        for (int i = 0; i < bytes.Length;) ids.Add(new NameID((ulong)Data.ToInt64(bytes[i..(i += sizeof(ulong))], false)));
        return ids.ToArray();
    }

    private void PackColorPalettes(out NameID[] resourceIDs, out byte[] resourceData)
    {
        resourceIDs = this.colorPalettes.GetCodes();
        ColorPalette[] colorPalettes = this.colorPalettes.GetValues();
        AutoSizedArray<byte> data = new(0xFF);

        for (int i = 0; i < resourceIDs.Length; i++)
        {
            ColorPalette colorPalette = colorPalettes[i];
            Color[] colors = colorPalette.colors.ToArray();
            data.Add(Data.GetBytes(colors.Length, true));

            bool grayscale = colorPalette.Grayscale(), uniformAlpha = colorPalette.UniformAlpha();

            data.Add((byte)(grayscale ? 1 : 0));
            data.Add((byte)(uniformAlpha ? 1 : 0));
            if (uniformAlpha) data.Add(colors[0].a);

            if (grayscale) data.Add(uniformAlpha ?
                Color.GetBytesGrayscale(colors) :
                Color.GetBytesGrayscaleWithAlpha(colors));
            else data.Add(uniformAlpha ?
                Color.GetBytes24(colors) :
                Color.GetBytes(colors, false));
        }
        resourceData = data.ToArray();
    }

    private void PackColorTextures(out NameID[] resourceIDs, out byte[] resourceData)
    {
        resourceIDs = this.colorTextures.GetCodes();
        ColorTexture[] colorTextures = this.colorTextures.GetValues();
        AutoSizedArray<byte> data = new(0xFF);

        for (int i = 0; i < resourceIDs.Length; i++)
        {
            ColorTexture colorTexture = colorTextures[i];
            data.Add(colorTexture.Size.GetBytes(true));

            bool grayscale = colorTexture.Grayscale(), uniformAlpha = colorTexture.UniformAlpha();

            data.Add((byte)(grayscale ? 1 : 0));
            data.Add((byte)(uniformAlpha ? 1 : 0));
            if (uniformAlpha) data.Add(colorTexture.pixels[0].a);

            if (grayscale) data.Add(uniformAlpha ?
                Color.GetBytesGrayscale(colorTexture.pixels) :
                Color.GetBytesGrayscaleWithAlpha(colorTexture.pixels));
            else data.Add(uniformAlpha ?
                Color.GetBytes24(colorTexture.pixels) :
                Color.GetBytes(colorTexture.pixels, false));
        }
        resourceData = data.ToArray();
    }

    private void PackIndexPalettes(out NameID[] resourceIDs, out byte[] resourceData)
    {
        resourceIDs = this.indexPalettes.GetCodes();
        byte[][] indexPalettes = this.indexPalettes.GetValues();
        AutoSizedArray<byte> data = new(0xFF);

        for (int i = 0; i < resourceIDs.Length; i++)
        {
            byte[] indexPalette = indexPalettes[i];
            data.Add(Data.GetBytes(indexPalette.Length, true));

            byte max = Math.Max(indexPalette);

            byte bpi = 1;
                 if (max > 0b1111111) bpi = 8;//could this be simplified?
            else if (max > 0b111111) bpi = 7;
            else if (max > 0b11111) bpi = 6;
            else if (max > 0b1111) bpi = 5;
            else if (max > 0b111) bpi = 4;
            else if (max > 0b11) bpi = 3;
            else if (max > 0b1) bpi = 2;

            data.Add(bpi);
            data.Add(bpi < 8 ? Data.PackBits(bpi, indexPalette) : indexPalette);
        }
        resourceData = data.ToArray();
    }

    private void PackPalettizedTextures(out NameID[] resourceIDs, out byte[] resourceData)
    {
        resourceIDs = this.palettizedTextures.GetCodes();
        PalettizedTexture[] palettizedTextures = this.palettizedTextures.GetValues();
        AutoSizedArray<byte> data = new(0xFF);

        for (int i = 0; i < resourceIDs.Length; i++)
        {
            PalettizedTexture palettizedTexture = palettizedTextures[i];
            data.Add(palettizedTexture.Size.GetBytes(true));

            byte max = Math.Max(palettizedTexture.pixels);

            byte bpp = 1;
                 if (max > 0b1111111) bpp = 8;//could this be simplified?
            else if (max > 0b111111) bpp = 7;
            else if (max > 0b11111) bpp = 6;
            else if (max > 0b1111) bpp = 5;
            else if (max > 0b111) bpp = 4;
            else if (max > 0b11) bpp = 3;
            else if (max > 0b1) bpp = 2;

            data.Add(bpp);
            data.Add(bpp < 8 ? Data.PackBits(bpp, palettizedTexture.pixels) : palettizedTexture.pixels);
        }
        resourceData = data.ToArray();
    }

    private void PackWaveShapes(out NameID[] resourceIDs, out byte[] resourceData)
    {
        resourceIDs = this.waveShapes.GetCodes();
        Sample[][] waveShapes = this.waveShapes.GetValues();
        AutoSizedArray<byte> data = new(0xFF);

        for (int i = 0; i < resourceIDs.Length; i++)
        {
            Sample[] waveShape = waveShapes[i];
            data.Add(Data.GetBytes(waveShape.Length, true));

            bool stereo = false;
            foreach (Sample sample in waveShape) stereo = stereo || !sample.IsMono();//make this into a static method?

            if (stereo)
            {
                data.Add(0);
                data.Add(Sample.GetBytes(waveShape, true));
            }
            else
            {
                data.Add(1);
                data.Add(Sample.GetBytesMono(waveShape, true, false));
            }
        }
        resourceData = data.ToArray();
    }

    private int UnpackColorPalettes(NameID[] resourceIDs, byte[] resourceData, int startAddress)
    {
        int resourceCount = 0;
        foreach (NameID resourceID in resourceIDs)
        {
            int length = Data.ToInt32(resourceData[startAddress..(startAddress += sizeof(int))], true);

            bool grayscale = resourceData[startAddress++] == 1, uniformAlpha = resourceData[startAddress++] == 1;
            byte a = 0;
            if (uniformAlpha) a = resourceData[startAddress++];

            Color[] colors;
            if (grayscale) colors = uniformAlpha ?
                    Color.FromBytesMultipleGrayScale(resourceData[startAddress..(startAddress += sizeof(byte) * length)]) :
                    Color.FromBytesMultipleGrayScaleWithAlpha(resourceData[startAddress..(startAddress += sizeof(byte) * 2 * length)]);
            else colors = uniformAlpha ?
                    Color.FromBytesMultiple24(resourceData[startAddress..(startAddress += sizeof(byte) * 3 * length)]) :
                    Color.FromBytesMultiple(resourceData[startAddress..(startAddress += sizeof(byte) * 4 * length)], false);

            if (uniformAlpha) for (int i = 0; i < colors.Length; i++) colors[i].a = a;

            colorPalettes.Add(resourceID, new(colors));
            resourceCount++;
        }
        return resourceCount;
    }

    private int UnpackColorTextures(NameID[] resourceIDs, byte[] resourceData, int startAddress)
    {
        int resourceCount = 0;
        foreach (NameID resourceID in resourceIDs)
        {
            Vector dimensions = Vector.FromBytes(resourceData[startAddress..(startAddress += sizeof(int) * 2)], true);
            int length = dimensions.x * dimensions.y;

            bool grayscale = resourceData[startAddress++] == 1, uniformAlpha = resourceData[startAddress++] == 1;
            byte a = 0;
            if (uniformAlpha) a = resourceData[startAddress++];

            Color[] pixels;
            if (grayscale) pixels = uniformAlpha ?
                    Color.FromBytesMultipleGrayScale(resourceData[startAddress..(startAddress += sizeof(byte) * length)]) :
                    Color.FromBytesMultipleGrayScaleWithAlpha(resourceData[startAddress..(startAddress += sizeof(byte) * 2 * length)]);
            else pixels = uniformAlpha ?
                    Color.FromBytesMultiple24(resourceData[startAddress..(startAddress += sizeof(byte) * 3 * length)]) :
                    Color.FromBytesMultiple(resourceData[startAddress..(startAddress += sizeof(byte) * 4 * length)], false);

            if (uniformAlpha) for (int i = 0; i < pixels.Length; i++) pixels[i].a = a;

            colorTextures.Add(resourceID, new(dimensions, pixels));
            resourceCount++;
        }
        return resourceCount;
    }

    private int UnpackIndexPalettes(NameID[] resourceIDs, byte[] resourceData, int startAddress)
    {
        int resourceCount = 0;
        foreach (NameID resourceID in resourceIDs)
        {
            int length = Data.ToInt32(resourceData[startAddress..(startAddress += sizeof(int))], true);
            byte bpi = resourceData[startAddress++];

            byte[] indexData = null;

            if (bpi == 8) indexData = resourceData[startAddress..(startAddress += length)];
            else if (bpi < 8)
            {
                float packedLength = (length * bpi) / 8f;
                indexData = Data.UnpackBits(bpi, resourceData[startAddress..(startAddress += ((int)packedLength + (Math.HasFraction(packedLength) ? 1 : 0)))]);
                if (indexData.Length > length) indexData = indexData[..length];
            }

            indexPalettes.Add(resourceID, indexData);
            resourceCount++;
        }
        return resourceCount;
    }

    private int UnpackPalettizedTextures(NameID[] resourceIDs, byte[] resourceData, int startAdress)
    {
        int resourceCount = 0;
        foreach (NameID resourceID in resourceIDs)
        {
            Vector dimensions = Vector.FromBytes(resourceData[startAdress..(startAdress += sizeof(int) * 2)], true);
            byte bpp = resourceData[startAdress++];

            int length = dimensions.x * dimensions.y;
            byte[] pixelData = null;

            if (bpp == 8) pixelData = resourceData[startAdress..(startAdress += length)];
            else if (bpp < 8)
            {
                float packedLength = (length * bpp) / 8f;
                pixelData = Data.UnpackBits(bpp, resourceData[startAdress..(startAdress += ((int)packedLength + (Math.HasFraction(packedLength) ? 1 : 0)))]);
                if (pixelData.Length > length) pixelData = pixelData[..length];
            }

            palettizedTextures.Add(resourceID, new(dimensions, pixelData));
            resourceCount++;
        }
        return resourceCount;
    }

    private int UnpackWaveShapes(NameID[] resourceIDs, byte[] resourceData, int startAddress)
    {
        int resourceCount = 0;
        foreach (NameID resourceID in resourceIDs)
        {
            int length = Data.ToInt32(resourceData[startAddress..(startAddress += sizeof(int))], true);
            waveShapes.Add(resourceID, resourceData[startAddress++] == 0 ?
                Sample.FromBytesMultiple(resourceData[startAddress..(startAddress += sizeof(short) * 2 * length)], true) :
                Sample.FromBytesMultipleMono(resourceData[startAddress..(startAddress += sizeof(short) * length)], true));
            resourceCount++;
        }
        return resourceCount;
    }

    public byte[] Pack()
    {
        AutoSizedArray<TypeSubHeader> typeSubHeaders = new(/*?*/);
        AutoSizedArray<NameID> resourceIDs = new(/*?*/);
        AutoSizedArray<byte> resourceData = new(/*?*/);

        foreach (ResourceDelegates resourceDelegate in delegates.GetValues())
        {
            resourceDelegate.pack.Invoke(out NameID[] idBuffer, out byte[] dataBuffer);
            if (idBuffer.Length > 0)
            {
                typeSubHeaders.Add(new TypeSubHeader()
                {
                    typeID = resourceDelegate.id,
                    resourceCount = (ushort)idBuffer.Length,
                    index = resourceIDs.Length,
                    address = resourceData.Length
                });
                resourceIDs.Add(idBuffer);
                resourceData.Add(dataBuffer);
            }
        }

        AutoSizedArray<byte> finalBuffer = new(new byte[]
        {
            //P     R     J     C     T     f     o     x
            0x50, 0x52, 0x4A, 0x43, 0x54, 0x66, 0x6F, 0x78,
            0, 0, 0, 0, //file size
            0, 0, //resource type count
        } /*?*/);

        if (typeSubHeaders.Length > 0)
        {
            finalBuffer.ReplaceRange(0xC, 2, Data.GetBytes((short)(ushort)typeSubHeaders.Length, true));
            finalBuffer.Add(TypeSubHeader.GetBytes(typeSubHeaders.ToArray()));
        }
        if (resourceIDs.Length > 0) finalBuffer.Add(GetIDBytes(resourceIDs.ToArray()));
        if (resourceData.Length > 0) finalBuffer.Add(resourceData.ToArray());

        finalBuffer.ReplaceRange(8, 4, Data.GetBytes(finalBuffer.Length, true));

        return finalBuffer.ToArray();
    }

    public int Unpack(byte[] data)
    {
        if (data == null || data.Length < 0xE) return -1;

        int address = 0;

        ulong header = (ulong)Data.ToInt64(data[..(address += sizeof(ulong))], false);
        if (header != Header) return -1;

        uint size = (uint)Data.ToInt32(data[address..(address += sizeof(uint))], true);
        if (data.Length != size) return -1;

        ushort typeSubHeaderCount = (ushort)Data.ToInt16(data[address..(address += sizeof(ushort))], true);

        TypeSubHeader[] typeSubHeaders = TypeSubHeader.FromBytes(data[address..(address += typeSubHeaderCount * TypeSubHeader.Size)]);

        int resourceCount = 0;
        foreach (TypeSubHeader typeSubHeader in typeSubHeaders) resourceCount += typeSubHeader.resourceCount;

        NameID[] resourceIDs = GetIDs(data[address..(address += resourceCount * sizeof(ulong))]);
        byte[] resourceData = data[address..];

        resourceCount = 0;
        foreach (TypeSubHeader typeSubHeader in typeSubHeaders)
            if (delegates.TryGet(typeSubHeader.typeID, out ResourceDelegates resourceDelegates))
                resourceCount += resourceDelegates.unpack(resourceIDs[typeSubHeader.index..(typeSubHeader.index + typeSubHeader.resourceCount)], resourceData, typeSubHeader.address);

        return resourceCount;
    }
}