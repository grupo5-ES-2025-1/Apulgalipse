using UnityEngine;
using UnityEngine.InputSystem;

public class ShopKeeper : MonoBehaviour
{

    private bool playerInRange;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;
    public bool isShopOpen;

    void Update()
    {
        if (playerInRange)
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                if (!isShopOpen)
                {
                    isShopOpen = true;
                    Time.timeScale = 0;
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;
                }
                else
                {
                    isShopOpen = false;
                    Time.timeScale = 1;
                    shopCanvasGroup.alpha = 0;
                    shopCanvasGroup.blocksRaycasts = false;
                    shopCanvasGroup.interactable = false;
                }
                
            }
        }
    }

    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopManager.shopItems);
    }

    public void OpenWeaponShop()
    {
        shopManager.PopulateShopItems(shopManager.shopWeapons);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

     private void OnPointerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
