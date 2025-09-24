using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject player;
    public Enemy_Spawner enemyspawner;
    public GameObject GameOver;
    public GameObject StartWindow;
    public Button KeepPlay;
    
    [SerializeField] public ParticleSystem[] particles;

    private int click = 0;
    private Animator _playerAnime;
    private string _playerRunKey;
    private string _PlayerWakeUpKey;
    
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;

    public static int maxMoeny = 100000000;
    public static int money;

    private void Start()        //게임이시작하면
    {
        KeepPlay.onClick.AddListener(CountClick);
        player.SetActive(false);        //일단플레이어숨김
        enemyspawner.StopSpawning();    //일단적생성정지
        GameOver.SetActive(false);      //게임오버UI숨김
        StartWindow.SetActive(true);    //게임시작UI켜기
        //이미 배경은 멈춰있음

        _playerAnime = player.GetComponentInChildren<Animator>();  // 플레이어 애니메이션
        _playerRunKey = "IsRun";
        _PlayerWakeUpKey = "WakeUp";
        money = PlayerPrefs.GetInt("money", 0);
    }

    public void CountClick()
    {
        click++;
        if(click > 0)
        {
            KeepPlay.interactable = false;
        }
    }

    public void StartGame()     //게임시작버튼누르면
    {
        player.SetActive(true);     // 플레이어등장
        _playerAnime.SetBool(_playerRunKey, true);
        enemyspawner.StartSpawning(); // 적생성시작
        Ground.canMoving = true;
    }

    public void KeepGame()
    {
        CloudSpawner.isPlay = true;
        _canvasGroup.alpha = 1;
        _rectTransform.localScale = Vector3.one;
        
        _canvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear).SetUpdate(UpdateType.Normal, true);
        _rectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack).SetUpdate(UpdateType.Normal,
            true).OnComplete(() =>
        {
            _canvasGroup.gameObject.SetActive(false);
        });
        
        StaminaManager.instance.StaminaChange(70);
        StaminaManager.instance.StaminaPlus(0); // 스테미너 바를 갱신 하기 위함
        
        _playerAnime.SetTrigger(_PlayerWakeUpKey);  //일어나는동작
        _playerAnime.SetBool(_playerRunKey, true);  //달리는동작
        enemyspawner.StartSpawning(); // 적생성시작
        Ground.canMoving = true;
        
        foreach (var particle in particles) // 파티클 모두 시작
        {
            particle.Play();
        }
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("money", money);
        CloudSpawner.isPlay = false;
        _canvasGroup.alpha = 0;
        _rectTransform.localScale = Vector3.zero;

        _canvasGroup.DOFade(1, 0.3f).SetEase(Ease.Linear).SetUpdate(UpdateType.Normal, true);
        _rectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetUpdate(UpdateType.Normal, true);
        
        GameOver.SetActive(true);   // 게임오버UI켜짐
        enemyspawner.StopSpawning();    // 적생성정지
        Ground.canMoving = false;
        
        foreach (var particle in particles) // 파티클 모두 일시 정지
        {
            particle.Pause();
        }
    }
}
