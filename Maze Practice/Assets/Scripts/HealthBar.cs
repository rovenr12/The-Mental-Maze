using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fillImage;

    public void SetMaxHealth(float maxHealth) {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        fillImage.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health) {
        slider.value = health;
        fillImage.color = gradient.Evaluate(slider.normalizedValue);
    }
    
}