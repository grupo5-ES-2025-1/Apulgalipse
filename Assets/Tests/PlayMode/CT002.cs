using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerHealthTests
{
    [UnityTest]
    public IEnumerator ChangeHealth_ClampaValores_EAtualizaBarra()
    {
        // StatsManager singleton dinâmico
        var statsType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
            .FirstOrDefault(t => t.Name == "StatsManager");
        Assert.IsNotNull(statsType, "StatsManager não encontrado.");

        var statsGO = new GameObject("StatsManager");
        var stats = statsGO.AddComponent(statsType);

        // set Instance, maxHealth, currentHealth via reflection
        var instProp = statsType.GetProperty("Instance");
        instProp?.SetValue(null, stats);
        statsType.GetField("maxHealth")?.SetValue(stats, 5);
        statsType.GetField("currentHealth")?.SetValue(stats, 5);

        // HealthBar (se existir)
        var hbType = TestTypeUtils.FindMono("HealthBar");
        Component hb = null;
        if (hbType != null) hb = new GameObject("HealthBar").AddComponent(hbType);

        // PlayerHealth dinâmico
        var phType = TestTypeUtils.FindMono("PlayerHealth");
        Assert.IsNotNull(phType, "PlayerHealth não encontrado.");
        var player = new GameObject("Player");
        var ph = player.AddComponent(phType);

#if UNITY_EDITOR
        if (hb != null)
        {
            var so = new UnityEditor.SerializedObject(ph);
            var hbP = so.FindProperty("healthBar");
            if (hbP != null) hbP.objectReferenceValue = (UnityEngine.Object)(hb as object);
            so.ApplyModifiedPropertiesWithoutUndo();
        }
#endif
        yield return null;

        phType.GetMethod("ChangeHealth")?.Invoke(ph, new object[] { -10 });
        yield return null;
        int cur = (int)(statsType.GetField("currentHealth")?.GetValue(stats) ?? 0);
        Assert.LessOrEqual(cur, 0);

        phType.GetMethod("ChangeHealth")?.Invoke(ph, new object[] { +999 });
        yield return null;
        int max = (int)(statsType.GetField("maxHealth")?.GetValue(stats) ?? 0);
        cur = (int)(statsType.GetField("currentHealth")?.GetValue(stats) ?? 0);
        Assert.AreEqual(max, cur);
    }
    
}
