using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayTimeUI : MonoBehaviour
{
    public GameObject player;
    public TMP_Text playTimeText;  // 연결할 TMP 텍스트
    public GameObject UI1;    // 플레이타임을 측정할 UI
    public GameObject UI2;    // 플레이타임을 측정할 UI
    public GameObject UI3;    // 플레이타임을 측정할 UI
    public Button UI4;    
    public TMP_Text OVER;
    public TMP_Text CLAER;

    public float playTime; // 시작 시간
    private float stamina; // 스테미너 감소를 위한 시간
    private Animator _anime;

    private void Start()
    {
        _anime = player.GetComponentInChildren<Animator>();
        playTime = 120;
    }

    void Update()
    {
        // targetUI가 비활성화되어 있을 때만 시간 증가
        if (!UI1.activeSelf && !UI2.activeSelf && !UI3.activeSelf)
        {
            playTime -= Time.deltaTime;
            stamina += Time.deltaTime;
        }

        // 시, 분, 초 변환
        int minutes = (int)((playTime % 3600) / 60);
        int seconds = (int)(playTime % 60);

        // UI에 표시
        playTimeText.text = $"{minutes:00}:{seconds:00}";
        if(minutes == 0 && seconds == 0)
        {
            UI4.interactable = false;
            OVER.gameObject.SetActive(false);
            CLAER.gameObject.SetActive(true);
            FindObjectOfType<GameStartManager>().EndGame();
            FindObjectOfType<GameOverManager>().Score();
            _anime.speed = 0f;
        }

        if (stamina >= 1)
        {
            StaminaManager.instance.StaminaPlus(-3);
            stamina = 0;
        }
        
    }
}
