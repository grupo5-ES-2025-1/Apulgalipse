using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private WeaponType type;
    [SerializeField] private int damage = 1;

    // trigger da animação no Animator
    [SerializeField] private string attackTrigger = "Attack";

    // só usado se for ranged
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;

    // --- Getters ---
    public string WeaponName => weaponName;
    public WeaponType Type => type;
    public int Damage => damage;
    public string AttackTrigger => attackTrigger;
    public GameObject ProjectilePrefab => projectilePrefab;
    public float ProjectileSpeed => projectileSpeed;
}
