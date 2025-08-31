using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerFacingAndAttackPointTests
{
    [UnityTest]
    public IEnumerator FlipDirecao_ReposicionaAttackPoint()
    {
        var pmType = TestTypeUtils.FindMono("PlayerMovement");
        if (pmType == null) { Assert.Ignore("PlayerMovement não encontrado (compile errors / outro nome?)."); yield break; }

        var go = new GameObject("Player");
        var rb = go.AddComponent<Rigidbody2D>(); yield return null; rb.gravityScale = 0f;
        var sr = go.AddComponent<SpriteRenderer>();
        var anim = go.AddComponent<Animator>();
        var attackPoint = new GameObject("AttackPoint").transform; attackPoint.SetParent(go.transform, false);

        var pm = (MonoBehaviour)go.AddComponent(pmType);

#if UNITY_EDITOR
        var so = new UnityEditor.SerializedObject(pm);
        var rbP  = so.FindProperty("rb");          if (rbP  != null) rbP.objectReferenceValue  = rb;
        var anP  = so.FindProperty("animator");    if (anP  != null) anP.objectReferenceValue  = anim;
        var srP  = so.FindProperty("spriteRenderer"); if (srP!= null) srP.objectReferenceValue = sr;
        var apP  = so.FindProperty("attackPoint"); if (apP  != null) apP.objectReferenceValue  = attackPoint;
        var offP = so.FindProperty("attackOffsetX"); if (offP!= null && Mathf.Approximately(offP.floatValue, 0f)) offP.floatValue = 0.5f;
        so.ApplyModifiedPropertiesWithoutUndo();
#endif
        sr.flipX = false; yield return null; float rightX = attackPoint.localPosition.x;
        sr.flipX = true;  yield return null; float leftX  = attackPoint.localPosition.x;

        Assert.Greater(rightX, 0f);
        Assert.Less(leftX, 0f);
        Object.Destroy(go);
    }
}
