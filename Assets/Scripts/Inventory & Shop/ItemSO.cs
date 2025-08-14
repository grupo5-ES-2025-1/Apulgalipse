using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite icon;

    [Header("Stats")]
    public int damage;
    public int speed;
    public int stamina;
    public int maxHealth;
    public int defense;
    public int luck;

    [Header("Temporary Itens")]
    public float duration;
}
