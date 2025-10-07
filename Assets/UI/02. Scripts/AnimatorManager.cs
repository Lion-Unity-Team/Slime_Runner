using System;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private RuntimeAnimatorController[] animatorControllers;

    public static AnimatorManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    

    public void ChangeAnimator(int fileIndex)
    {
        int index = fileIndex - 1;

        if (index >= 0 && index < animatorControllers.Length)
        {
            playerAnimator.runtimeAnimatorController = animatorControllers[index];
            PlayerPrefs.SetInt("CurrentSkin", fileIndex);
            PlayerPrefs.Save();
            Debug.Log($"[AnimatorManager] {fileIndex}번 애니메이터로 교체 완료");
            
        }
        else
        {
            Debug.LogWarning($"[AnimatorManager] 잘못된 FileIndex 요청: {fileIndex}");
        }
    }
}
