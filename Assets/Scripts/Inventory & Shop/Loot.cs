using Unity.VisualScripting;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemSO itemSo;
    public SpriteRenderer sr;
    public Animator anim;

    public int quantity;

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
            Destroy(gameObject, .5f);
        }
    }
}
