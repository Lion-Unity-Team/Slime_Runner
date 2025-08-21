using System;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public static StaminaManager instance;
    
    private Color maxStaminaColor = new Color32(192, 212, 112, 255);
    private Color minStaminaColor = new Color32(181,108,120,255);

    [SerializeField] private Image currentStaminaBar;
    
    private float currentStamina;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentStamina = currentStaminaBar.fillAmount;
    }


    public void StaminaChange(float value)
    {
        currentStamina += (value / 100);
        
        if (currentStamina > 1) // 스테미너가 1를 초과했을 경우
        {
            currentStamina = 1;
        }
        else if (currentStamina <= 0) // 스태미너가 0 이하일 경우
        {
            
        }
        currentStaminaBar.color = Color.Lerp(minStaminaColor, maxStaminaColor, currentStamina);
        currentStaminaBar.fillAmount = currentStamina;
    }
    
}
