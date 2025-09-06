using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;

    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    [Header("Stat Fields")]
    public TMP_Text[] statTexts;

    private RectTransform infoPanelRect;

    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSO itemSO)
    {
        infoPanel.alpha = 1;

        itemNameText.text = itemSO.itemName;
        itemDescriptionText.text = itemSO.itemDescription;

        List<string> stats = new List<string>();
        if (itemSO.currentHealth > 0) stats.Add("Health: " + itemSO.currentHealth.ToString());
        if (itemSO.maxHealth > 0) stats.Add("Max Health: " + itemSO.maxHealth.ToString());
        if (itemSO.damage > 0) stats.Add("Damage: " + itemSO.damage.ToString());
        if (itemSO.speed > 0) stats.Add("Speed: " + itemSO.speed.ToString());
        if (itemSO.defense > 0) stats.Add("Defense: " + itemSO.defense.ToString());
        if (itemSO.luck > 0) stats.Add("Luck: " + itemSO.luck.ToString());
        if (itemSO.stamina > 0) stats.Add("Stamina: " + itemSO.stamina.ToString());

        if (stats.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < statTexts.Length; i++)
        {
            if (i < stats.Count)
            {
                statTexts[i].text = stats[i];
                statTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statTexts[i].gameObject.SetActive(false);
            }
            
            
        }

        
    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    // public void FollowMouse()
    // {
    //     Vector3 mousePosition = Input.mousePosition;
    //     Vector3 offset = new(10, -10, 0);

    //     infoPanelRect.position = mousePosition + offset;
        
    // }

}
