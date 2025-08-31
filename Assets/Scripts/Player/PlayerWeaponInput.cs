using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponInput : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    private void Awake()
    {
        if (weaponManager == null)
            weaponManager = GetComponent<WeaponManager>();
    }

    // Métodos para cada tecla (configure no Input Actions)
    public void OnSelectWeapon1(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) weaponManager.SelectWeaponByKey(1);
    }
    public void OnSelectWeapon2(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) weaponManager.SelectWeaponByKey(2);
    }
    public void OnSelectWeapon3(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) weaponManager.SelectWeaponByKey(3);
    }
    public void OnSelectWeapon4(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) weaponManager.SelectWeaponByKey(4);
    }
}