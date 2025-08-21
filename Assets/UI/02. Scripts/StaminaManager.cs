using System;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public static StaminaManager instance;
    
    private Color maxStaminaColor = new Color32(192, 212, 112, 255);
    private Color minStaminaColor = new Color32(181,108,120,255);

    public Image currentStaminaBar;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    
    public void StaminaChange(float currentStamina)
    {
        currentStaminaBar.color = Color.Lerp(minStaminaColor, maxStaminaColor, currentStamina / 100);
        currentStaminaBar.fillAmount = currentStamina / 100;
    }
    
}
