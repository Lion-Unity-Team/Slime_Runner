using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSlime : MonoBehaviour
{
    private Animator _anime;
    public TMP_Text playerHpText;
    public static double playerHp;

    public Button UI1;
    public TMP_Text OVER;
    public TMP_Text CLAER;

    private string _deathAnimeKey;
    private string _runAnimeKey;

    private void Start()
    {
        playerHp = double.Parse(playerHpText.text);
        _anime = GetComponentInChildren<Animator>();

        _deathAnimeKey = "Death";
        _runAnimeKey = "IsRun";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TMP_Text enemyHpText = collision.GetComponentInChildren<TMP_Text>();
            if (enemyHpText == null) return;

            double enemyHp = double.Parse(enemyHpText.text);

            if (playerHp <= enemyHp)
            {
                FindObjectOfType<GameStartManager>().EndGame();
                FindObjectOfType<GameOverManager>().Score();
                _anime.SetTrigger(_deathAnimeKey);
                _anime.SetBool(_runAnimeKey, false);
            }
            else 
            {
                playerHp += enemyHp;
                if(playerHp >= 100000000)    //엔딩조건1 : 1억점 넘기기
                {
                    OVER.gameObject.SetActive(false);
                    CLAER.gameObject.SetActive(true);
                    playerHp = 100000000;
                    playerHpText.text = playerHp.ToString();
                    UI1.interactable=false;
                    FindObjectOfType<GameStartManager>().EndGame();
                    FindObjectOfType<GameOverManager>().Score();
                    _anime.speed = 0f;
                }
                playerHpText.text = playerHp.ToString();
            }
            StaminaManager.instance.StaminaChange(-20);
        }

        if (collision.CompareTag("Fruit"))
        {
            StaminaManager.instance.StaminaChange(30);
        }
        
        SoundManager.instance.SfxPlay("Eat");
    }

    void Update()
    {
        string value = playerHpText.text;
        int digitCount = value.Length;

        float fontSize = 1f;

        if (digitCount >= 15)
            fontSize = 0.35f;
        else if (digitCount >= 14)
            fontSize = 0.37f;
        else if (digitCount >= 13)
            fontSize = 0.4f;
        else if (digitCount >= 12)
            fontSize = 0.45f;
        else if (digitCount >= 11)
            fontSize = 0.5f;
        else if (digitCount >= 10)
            fontSize = 0.55f;
        else if (digitCount >= 9)
            fontSize = 0.6f;
        else if (digitCount >= 8)
            fontSize = 0.7f;
        else if (digitCount >= 7)
            fontSize = 0.8f;
        else if (digitCount >= 6)
            fontSize = 0.9f;

        playerHpText.fontSize = fontSize;
    }
}
