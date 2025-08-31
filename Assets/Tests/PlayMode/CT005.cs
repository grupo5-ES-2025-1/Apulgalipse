using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PauseMenuTests
{
    [UnityTest]
    public IEnumerator ResumeGame_RetomaTimeScale_EscondeMenu()
    {
        var pmType = TestTypeUtils.FindMono("PauseMenu");
        Assert.IsNotNull(pmType, "PauseMenu não encontrado.");

        var root = new GameObject("PauseRoot");
        var panel = new GameObject("PausePanel");
        panel.transform.SetParent(root.transform, false);

        var pm = (MonoBehaviour)root.AddComponent(pmType);

#if UNITY_EDITOR
        try{
            var so = new UnityEditor.SerializedObject(pm);
            var p = so.FindProperty("pauseMenu"); if (p != null) p.objectReferenceValue = panel;
            so.ApplyModifiedPropertiesWithoutUndo();
        }catch{}
#endif

        panel.SetActive(true);
        Time.timeScale = 0f;

        pmType.GetMethod("ResumeGame")?.Invoke(pm, null);
        yield return null;

        Assert.IsFalse(panel.activeSelf, "Painel de pause deve esconder após ResumeGame.");
        Assert.AreEqual(1f, Time.timeScale, "TimeScale deve voltar a 1.");
    }
}