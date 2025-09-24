using DG.Tweening;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public Slime_Movement playerMovement;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;

    public void OpenSettings()
    {
        playerMovement.canMove = false;
        Ground.canMoving = false;
        Time.timeScale = 0;

        _canvasGroup.alpha = 0;
        _rectTransform.localScale = Vector3.zero;

        _canvasGroup.DOFade(1, 0.3f).SetEase(Ease.Linear).SetUpdate(UpdateType.Normal, true);
        _rectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetUpdate(UpdateType.Normal, true);
    }

    public void CloseSettings()
    {
        playerMovement.canMove = true;
        Ground.canMoving = true;
        Time.timeScale = 1;
        
        _canvasGroup.alpha = 1;
        _rectTransform.localScale = Vector3.one;
        
        _canvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear).SetUpdate(UpdateType.Normal, true);
        _rectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack).SetUpdate(UpdateType.Normal,
            true).OnComplete(() =>
        {
            _canvasGroup.gameObject.SetActive(false);
        });
    }
}
