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
    
    public void StaminaPlus(float value) // 현재 스테미너에 value 값 만큼 더하는 함수
    {
        currentStamina += (value / 100);
        
        if (currentStamina > 1) // 스테미너가 1를 초과했을 경우
        {
            currentStamina = 1;
        }
        
        currentStaminaBar.color = Color.Lerp(minStaminaColor, maxStaminaColor, currentStamina);
        currentStaminaBar.fillAmount = currentStamina;
    }

    public void StaminaChange(float value) // 현재 스테미너를 value 값으로 만드는 함수
    {
        currentStamina = value / 100;
    }

    public float GetCurrentStamina() // 현재 스테미너를 반환하는 함수
    {
        return currentStamina;
    }
    
}
