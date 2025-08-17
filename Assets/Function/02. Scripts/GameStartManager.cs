using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject player;
    public Enemy_Spawner enemyspawner;
    public GameObject GameOver;
    public GameObject StartWindow;
    public Button KeepPlay;

    private int click = 0;
    private Animator _playerAnime;
    private string _playerRunKey;
    private string _PlayerWakeUpKey;

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
        _playerAnime.SetTrigger(_PlayerWakeUpKey);  //일어나는동작
        _playerAnime.SetBool(_playerRunKey, true);  //달리는동작
        enemyspawner.StartSpawning(); // 적생성시작
        Ground.canMoving = true;
    }

    public void EndGame()
    {
        GameOver.SetActive(true);   // 게임오버UI켜짐
        enemyspawner.StopSpawning();    // 적생성정지
        Ground.canMoving = false;
    }
}
