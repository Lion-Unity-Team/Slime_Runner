using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private RuntimeAnimatorController[] animatorControllers;

    public static AnimatorManager Instance { get; private set; }

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// 셀에서 전달된 인덱스를 기반으로 플레이어 애니메이터 교체
    /// </summary>
    public void ChangeAnimator(int fileIndex)
    {
        int index = fileIndex - 1; // File Index가 1~18이므로 배열 인덱스(0~17)로 변환

        if (index >= 0 && index < animatorControllers.Length)
        {
            playerAnimator.runtimeAnimatorController = animatorControllers[index];
            Debug.Log($"[AnimatorManager] {fileIndex}번 애니메이터로 교체 완료");
        }
        else
        {
            Debug.LogWarning($"[AnimatorManager] 잘못된 FileIndex 요청: {fileIndex}");
        }
    }
}
