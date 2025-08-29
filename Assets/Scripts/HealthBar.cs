using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;


    public void SetMaxHealth(int health)
    {
        if (slider != null)
        {
            slider.maxValue = health;
            slider.value = health;
            //fill.color = gradient.Evaluate(1f);
        }

    }

    public void SetHealth(int health)
    {
        Debug.Log($"Atualizando barra de vida para: {health}");
        if (slider != null) {
            slider.value = health;
        }

    }
    
}
