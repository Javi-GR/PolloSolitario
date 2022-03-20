using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    //Represents the slider component the healthbar has
    public Slider slider;
    //Makes the slider change color when the health goes down
    public Gradient gradient;
    //The atcual image fill that is in the slider
    public Image fill;
    //Sets the max health of the player the parameter value
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    //Sets the health of the character to a new value in the UI
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
