#if UNITY_EDITOR
using NUnit.Framework;
using UnityEditor;

public class BuildScenesTests
{
    [Test]
    public void CenasEssenciaisEstaoNaBuild()
    {
        string[] exigidas = {
            "Assets/Scenes/Main Menu.unity",
        };
        var build = EditorBuildSettings.scenes;
        foreach (var path in exigidas)
        {
            bool tem = System.Array.Exists(build, s => s.path == path && s.enabled);
            Assert.IsTrue(tem, $"Cena ausente/disabled: {path}");
        }
    }
}
#endif
