using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject player;
    public TMP_Text playTimeText;  // ������ TMP �ؽ�Ʈ
    public GameObject UI1;    // �÷���Ÿ���� ������ UI
    public GameObject UI2;    // �÷���Ÿ���� ������ UI
    public GameObject UI3;    // �÷���Ÿ���� ������ UI
    public Button UI4;
    public TMP_Text OVER;
    public TMP_Text CLAER;

    public float playTime; // ���� �ð�
    private float stamina; // ���׹̳� ���Ҹ� ���� �ð�
    private Animator _anime;
    public int playTime2 = 0;
    public float elapsedTime = 0f;

    private bool flag;
    private void Start()
    {
        _anime = player.GetComponentInChildren<Animator>();
        playTime = 120;
    }

    void Update()
    {
        // targetUI�� ��Ȱ��ȭ�Ǿ� ���� ���� �ð� ����
        if (!UI1.activeSelf && !UI2.activeSelf && !UI3.activeSelf)
        {
            playTime -= Time.deltaTime;
            stamina += Time.deltaTime;
            elapsedTime += Time.deltaTime;
            playTime2 = (int)elapsedTime;
        }

        // ��, ��, �� ��ȯ
        int minutes = (int)((playTime % 3600) / 60);
        int seconds = (int)(playTime % 60);

        // UI�� ǥ��
        playTimeText.text = $"{minutes:00}:{seconds:00}";
        if (minutes == 0 && seconds == 0 && !flag)
        {
            flag = true;
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
