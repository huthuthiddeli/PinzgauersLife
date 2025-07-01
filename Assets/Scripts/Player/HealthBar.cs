using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;



    public void SetMaxHealth(float health)
    {
        this.slider.maxValue = health;
        this.slider.value = health;
    }

    public void SetHealth(float health)
    {
        this.slider.value = health;
    }


}
