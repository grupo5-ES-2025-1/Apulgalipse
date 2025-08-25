using System.Collections;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void ApplyItemEffects(ItemSO itemSO)
    {
        if (itemSO.currentHealth > 0)
            StatsManager.Instance.UpdateCurrentHealth(itemSO.currentHealth);

        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.maxHealth);

        if (itemSO.speed > 0)
            StatsManager.Instance.UpdateSpeed(itemSO.speed);

        if (itemSO.defense > 0)
            StatsManager.Instance.UpdateDefense(itemSO.defense);

        if (itemSO.luck > 0)
            StatsManager.Instance.UpdateLuck(itemSO.luck);

        if (itemSO.damage > 0)
            StatsManager.Instance.UpdateDamage(itemSO.damage);

        if (itemSO.stamina > 0)
            StatsManager.Instance.UpdateStamina(itemSO.stamina);


        if (itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));

    }

    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (itemSO.currentHealth > 0)
            StatsManager.Instance.UpdateCurrentHealth(-itemSO.currentHealth);

        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(-itemSO.maxHealth);

        if (itemSO.speed > 0)
            StatsManager.Instance.UpdateSpeed(-itemSO.speed);

        if (itemSO.defense > 0)
            StatsManager.Instance.UpdateDefense(-itemSO.defense);

        if (itemSO.luck > 0)
            StatsManager.Instance.UpdateLuck(-itemSO.luck);

        if (itemSO.damage > 0)
            StatsManager.Instance.UpdateDamage(-itemSO.damage);

    }
}
