#if UNITY_EDITOR
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class PixelImportSettingsTests
{
    [Test]
    public void SpritesPrincipais_ComFiltroPointSemCompressao()
    {
        string[] assets =
        {
            "Assets/Art/Sprites/boy-player-sprite.png",
            "Assets/Art/Sprites/flea-sprite.png"
        };

        foreach (var path in assets)
        {
            var importer = (TextureImporter)AssetImporter.GetAtPath(path);
            Assert.IsNotNull(importer, $"Importer nulo para {path}");
            Assert.AreEqual(FilterMode.Point, importer.filterMode, $"{path}: use FilterMode.Point");
            Assert.AreEqual(TextureImporterCompression.Uncompressed, importer.textureCompression, $"{path}: desative compressão");
        }
    }
}
#endif
