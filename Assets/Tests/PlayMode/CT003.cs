using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyDamagePlayerTests
{
    [UnityTest]
    public IEnumerator ColisaoComEnemy_ReduzVidaPlayer()
    {
        var statsType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
            .FirstOrDefault(t => t.Name == "StatsManager");
        Assert.IsNotNull(statsType, "StatsManager não encontrado.");

        var statsGO = new GameObject("StatsManager");
        var stats = statsGO.AddComponent(statsType);
        statsType.GetProperty("Instance")?.SetValue(null, stats);
        statsType.GetField("maxHealth")?.SetValue(stats, 5);
        statsType.GetField("currentHealth")?.SetValue(stats, 5);

        var player = new GameObject("Player"); 
        player.tag = "Player";
        var rbP = player.AddComponent<Rigidbody2D>(); yield return null; rbP.gravityScale = 0;
        player.AddComponent<BoxCollider2D>();

        var phType = TestTypeUtils.FindMono("PlayerHealth");
        Assert.IsNotNull(phType, "PlayerHealth não encontrado.");
        var ph = player.AddComponent(phType);

        // 🔹 injeta HealthBar fake
        var hbType = TestTypeUtils.FindMono("HealthBar");
        if (hbType != null)
        {
            var hb = new GameObject("HealthBar").AddComponent(hbType);
#if UNITY_EDITOR
            var so = new UnityEditor.SerializedObject(ph);
            var hbProp = so.FindProperty("healthBar");
            if (hbProp != null) hbProp.objectReferenceValue = hb as UnityEngine.Object;
            so.ApplyModifiedPropertiesWithoutUndo();
#endif
        }

        yield return null; // deixa Start() rodar

        var enemy = new GameObject("Enemy");
        var rbE = enemy.AddComponent<Rigidbody2D>(); yield return null; rbE.gravityScale = 0;
        enemy.AddComponent<BoxCollider2D>();

        var ecType = TestTypeUtils.FindMono("EnemyCombat");
        Assert.IsNotNull(ecType, "EnemyCombat não encontrado.");
        var ec = enemy.AddComponent(ecType);
        var dmgField = ecType.GetField("damage"); if (dmgField != null) dmgField.SetValue(ec, 1);

        int antes = (int)(statsType.GetField("currentHealth")?.GetValue(stats) ?? 0);
        player.transform.position = Vector3.zero;
        enemy.transform.position = new Vector3(0.1f, 0, 0);

        yield return new WaitForFixedUpdate();

        int depois = (int)(statsType.GetField("currentHealth")?.GetValue(stats) ?? 0);
        Assert.Less(depois, antes, "Vida do player deveria diminuir após colisão com Enemy.");
    }
}