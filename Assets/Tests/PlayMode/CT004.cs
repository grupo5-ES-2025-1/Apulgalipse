using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LootPickupTests
{
    [UnityTest]
    public IEnumerator EntrarNaAreaDeLoot_DisparaEvento_OnItemLooted()
    {
    
        var lootType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
            .FirstOrDefault(t => t.Name == "Loot" && typeof(MonoBehaviour).IsAssignableFrom(t));
        Assert.IsNotNull(lootType, "Tipo 'Loot' não encontrado.");

        var itemSoType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
            .FirstOrDefault(t => t.Name == "ItemSO" && typeof(ScriptableObject).IsAssignableFrom(t));
       

   
        var evt = lootType.GetEvent("OnItemLooted");
        Assert.IsNotNull(evt, "Evento estático Loot.OnItemLooted não encontrado.");

        bool eventoChamado = false;
        var handler = new Action<object, int>((item, qty) => { eventoChamado = true; });
        var del = Delegate.CreateDelegate(evt.EventHandlerType, handler.Target, handler.Method);
        evt.AddEventHandler(null, del);

        var player = new GameObject("Player");
        player.tag = "Player";
        var rbP = player.AddComponent<Rigidbody2D>();
        rbP.bodyType = RigidbodyType2D.Kinematic;
        var colP = player.AddComponent<BoxCollider2D>();
        colP.isTrigger = false;

        var lootGO = new GameObject("Loot");
        var rbL = lootGO.AddComponent<Rigidbody2D>();
        rbP.bodyType = RigidbodyType2D.Kinematic;
        // rbP.isKinematic = true; // Substituído por bodyType

        rbL.bodyType = RigidbodyType2D.Kinematic;
        var colL = lootGO.AddComponent<BoxCollider2D>();
        colL.isTrigger = true;

        var loot = lootGO.AddComponent(lootType);
        var anim = lootGO.AddComponent<Animator>();

#if UNITY_EDITOR
        try
        {
            var so = new UnityEditor.SerializedObject(loot);

            var animProp = so.FindProperty("anim");
            if (animProp != null) animProp.objectReferenceValue = anim;

            if (itemSoType != null)
            {
                var fakeItem = ScriptableObject.CreateInstance(itemSoType);
                var itemProp = so.FindProperty("item");
                if (itemProp != null) itemProp.objectReferenceValue = fakeItem as UnityEngine.Object;
            }

            var qtyProp = so.FindProperty("amount") ?? so.FindProperty("quantity") ?? so.FindProperty("count");
            if (qtyProp != null && qtyProp.propertyType == UnityEditor.SerializedPropertyType.Integer)
                qtyProp.intValue = 1;

            so.ApplyModifiedPropertiesWithoutUndo();
        }
        catch {}
#endif

        lootGO.transform.position = Vector3.zero;
        player.transform.position = new Vector3(-1f, 0f, 0f);
        rbP.linearVelocity = new Vector2(5f, 0f);

        int safety = 60;
        while (!eventoChamado && safety-- > 0)
            yield return new WaitForFixedUpdate();

        rbP.linearVelocity = Vector2.zero;

        evt.RemoveEventHandler(null, del);

        Assert.IsTrue(eventoChamado, "Esperava que Loot.OnItemLooted fosse disparado ao entrar no trigger.");
    }
}