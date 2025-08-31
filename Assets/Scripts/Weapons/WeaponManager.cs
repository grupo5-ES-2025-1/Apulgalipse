using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon[] availableWeapons;
    [SerializeField] private int currentIndex = 0;

    private Weapon currentWeapon;

    private void Start()
    {
        if (availableWeapons == null || availableWeapons.Length == 0)
        {
            Debug.LogWarning("WeaponManager: availableWeapons vazio.");
            return;
        }
        EquipWeapon(0);
    }

    public Weapon GetCurrentWeapon() => currentWeapon;

    public void EquipWeapon(int index)
    {
        if (availableWeapons == null || availableWeapons.Length == 0) return;
        if (index < 0 || index >= availableWeapons.Length) return;

        currentIndex = index;
        currentWeapon = availableWeapons[index];
        Debug.Log("Equipped: " + currentWeapon.WeaponName);
    }

    public void NextWeapon() => EquipWeapon((currentIndex + 1) % availableWeapons.Length);
    public void PreviousWeapon() => EquipWeapon((currentIndex - 1 + availableWeapons.Length) % availableWeapons.Length);

    // NOVO: Seleciona arma pelas teclas 1-4
    public void SelectWeaponByKey(int key)
    {
        int index = key - 1; // Tecla 1 = índice 0
        EquipWeapon(index);
    }
}
