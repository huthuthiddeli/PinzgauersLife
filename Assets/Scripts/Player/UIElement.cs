using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIElement : MonoBehaviour
{
    public Slider slider;
    public TMP_Text textMesh;


    public void SetMaxValue(float health)
    {
        this.slider.maxValue = health;
        this.slider.value = health;
    }

    public void SetValue(float health)
    {
        this.slider.value = health;
    }

    public void SetText(string txt)
    {
        this.textMesh.text = txt;
    }

}
