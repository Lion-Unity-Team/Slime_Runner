using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public static StaminaManager instance;
    
    private Color maxStaminaColor = new Color32(192, 212, 112, 255);
    private Color minStaminaColor = new Color32(181,108,120,255);

    [SerializeField] private Image currentStaminaBar;
    [SerializeField] private Image staminaBar;

    private Vector3 staminaBarPos;
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
        staminaBarPos = staminaBar.transform.position;
    }
    
    public void StaminaPlus(float value) // 현재 스테미너에 value 값 만큼 더하는 함수
    {
        currentStamina += (value / 100);
        
        if (currentStamina > 1) // 스테미너가 1를 초과했을 경우
        {
            currentStamina = 1;
        }
        
        currentStaminaBar.color = Color.Lerp(minStaminaColor, maxStaminaColor, currentStamina);
        // currentStaminaBar.fillAmount = currentStamina;
        currentStaminaBar.DOFillAmount(currentStamina, 0.5f).SetEase(Ease.OutQuad);
        if (value < -10 || value > 0)
        {
            staminaBar.transform.DOShakePosition(duration: 0.5f, strength: 10, vibrato: 10, randomness: 90).OnComplete(() =>
            {
                staminaBar.transform.position = staminaBarPos;
            });
        }
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
