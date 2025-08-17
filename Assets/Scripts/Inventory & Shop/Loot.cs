using Unity.VisualScripting;
using UnityEngine;
using System;

public class Loot : MonoBehaviour
{
    public ItemSO itemSo;
    public SpriteRenderer sr;
    public Animator anim;

    public int quantity;
    public static event Action<ItemSO, int> OnItemLooted;

    private void OnValidate()
    {
        if (itemSo == null)
        {
            return;
        }

        sr.sprite = itemSo.icon;
        this.name = itemSo.itemName;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("LootPickup");
            OnItemLooted?.Invoke(itemSo, quantity);
            Destroy(gameObject, .5f);
        }
    }
}
