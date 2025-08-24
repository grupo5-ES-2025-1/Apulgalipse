using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;
    private bool statsOpen = false;

    private void Start()
    {
        UpdateAllStats();
    }

    private void Update()
    {
        // Press T to toggle the Stat Menu
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            if (statsOpen)
            {
                Time.timeScale = 1;
                statsOpen = false;
                statsCanvas.alpha = 0;
            }
            else
            {
                Time.timeScale = 0;
                statsOpen = true;
                statsCanvas.alpha = 1;

            }
        }
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
        UpdateHealth();
        UpdateDefense();
        UpdateStamina();
        UpdateLuck();
    }

    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance.damage;
    }

    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance.speed;
    }

    public void UpdateHealth()
    {
        statsSlots[2].GetComponentInChildren<TMP_Text>().text = "Health: " + StatsManager.Instance.maxHealth;
    }

    public void UpdateDefense()
    {
        statsSlots[3].GetComponentInChildren<TMP_Text>().text = "Defense: " + StatsManager.Instance.defense;

    }

    public void UpdateStamina()
    {
        statsSlots[4].GetComponentInChildren<TMP_Text>().text = "Stamina: " + StatsManager.Instance.stamina;
    }

    public void UpdateLuck()
    {
        statsSlots[5].GetComponentInChildren<TMP_Text>().text = "Luck: " + StatsManager.Instance.luck;
    }



}
