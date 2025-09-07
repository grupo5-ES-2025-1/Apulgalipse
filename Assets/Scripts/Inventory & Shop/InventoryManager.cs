using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{

    public InventorySlot[] itemSlots;
    public int gold;
    public TMP_Text goldText;
    public UseItem useItem;
    public GameObject lootPrefab;
    public Transform player;

    public CanvasGroup canvasGroup;
    private bool statsOpen = false;

    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUI();
        }
    }

    private void Update()
    {
        // Press I to toggle the Inventory
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            if (statsOpen)
            {
                // Time.timeScale = 1;
                statsOpen = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
                canvasGroup.alpha = 0;
            }
            else
            {
                // Time.timeScale = 0;
                statsOpen = true;
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;

            }
        }
    }

    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }

        foreach (var slot in itemSlots)
        {
            if (slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
            {
                int availableSpace = itemSO.stackSize - slot.quantity;

                int amountToAdd = Mathf.Min(availableSpace, quantity);

                slot.quantity += amountToAdd;
                quantity -= amountToAdd;

                slot.UpdateUI();

                if (quantity <= 0)
                    return;


            }
        }

        foreach (var slot in itemSlots)
        {
            if (slot.itemSO == null)
            {
                int amountToAdd = Mathf.Min(itemSO.stackSize - quantity);

                slot.itemSO = itemSO;
                slot.quantity = quantity;
                slot.UpdateUI();
                return;
            }
        }

        if (quantity > 0)
        {
            DropLoot(itemSO, quantity);
        }
        


    }

    public void DropItem(InventorySlot slot)
    {
        DropLoot(slot.itemSO, 1);
        slot.quantity--;

        if (slot.quantity <= 0)
        {
            slot.itemSO = null;
        }

        slot.UpdateUI();
    }

    private void DropLoot(ItemSO itemSO, int quantity)
    {
        Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO, quantity);
    }

    public void UseItem(InventorySlot slot)
    {
        if (slot.itemSO != null & slot.quantity >= 0)
        {
            Debug.Log("Trying to use item: " + slot.itemSO.itemName);
            useItem.ApplyItemEffects(slot.itemSO);

            slot.quantity--;
            Debug.Log("Item quantity: " + slot.quantity);
            if (slot.quantity <= 0)
            {
                slot.itemSO = null;
            }
            slot.UpdateUI();
        }
    }
}
