using C = System.Console;
using F = System.IO.File;
using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine;

namespace ProjectFox.TestBed;

public static partial class GameEngineTest
{
    private const string resourceFilePath = "TestResourceFile";

    public static void ResourceFileTest()
    {
        Debug.Console.Shutdown();

        ResourceFile resourceFile = new();

        resourceFile.waveShapes.Add(new("TstShpe", 0), new Sample[] { new(8), new(8) });
        resourceFile.waveShapes.Add(new("TstShpe", 1), new Sample[] { new(4, 2), new(3, 1) });
        //resourceFile.waveShapes.Add(new("TstShpe", 2), TestShape);

        resourceFile.colorTextures.Add(new("TstTxtr", 0), new(1, 1, new Color[] { 0xFFEEDDCC }));
        resourceFile.colorTextures.Add(new("TstTxtr", 1), new(2, 2, new Color[] { 0xFF0000FF, 0xFF0000FF, 0xFF0000FF, 0xFF0000FF, }));
        resourceFile.colorTextures.Add(new("TstTxtr", 2), new(2, 2, new Color[] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, }));
        resourceFile.colorTextures.Add(new("TstTxtr", 3), new(2, 2, new Color[] { 0xFF000080, 0x00FF0080, 0x0000FF80, 0x80000080, }));

        resourceFile.palettizedTextures.Add(new("TstTxtr", 4), new(1, 1, new byte[] { 0 }));
        resourceFile.palettizedTextures.Add(new("TstTxtr", 5), new(2, 1, new byte[] { 1, 2 }));
        resourceFile.palettizedTextures.Add(new("TstTxtr", 6), new(3, 3, new byte[] { 0, 31, 0,  31, 0, 31,  0, 31, 0, }));
        resourceFile.palettizedTextures.Add(new("TstTxtr", 7), new(2, 1, new byte[] { 255, 255 }));

        resourceFile.colorPalettes.Add(new("TstPlte", 0), new(0xFF0000FF, 0x00FF00FF));
        resourceFile.colorPalettes.Add(new("TstPlte", 1), new(0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFF00));
        resourceFile.colorPalettes.Add(new("TstPlte", 2), new(0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF));
        resourceFile.colorPalettes.Add(new("TstPlte", 3), new(0xFF000080, 0x00FF0080, 0x0000FF80));

        resourceFile.indexPalettes.Add(new("TstPlte", 4), new byte[] { 0 });
        resourceFile.indexPalettes.Add(new("TstPlte", 5), new byte[] { 1, 2 });
        resourceFile.indexPalettes.Add(new("TstPlte", 6), new byte[] { 0, 31, 0 });
        resourceFile.indexPalettes.Add(new("TstPlte", 7), new byte[] { 255, 255 });

        F.WriteAllBytes(resourceFilePath, resourceFile.Pack());

        resourceFile = new();
        C.WriteLine(resourceFile.Unpack(F.ReadAllBytes(resourceFilePath)));
        C.WriteLine(resourceFile.waveShapes.Concat());
        C.WriteLine(resourceFile.colorTextures.Concat());
        C.WriteLine(resourceFile.palettizedTextures.Concat());
        C.WriteLine(resourceFile.colorPalettes.Concat());
        C.WriteLine(resourceFile.indexPalettes.Concat());
    }
}