using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
    public static PortraitManager instance;
    
    [SerializeField] private Image[] images;
    [SerializeField] private TextMeshProUGUI[] names;
    [SerializeField] private TextMeshProUGUI[] exps; // 설명

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("BestPlayerHP"))
        {
            Debug.Log("로드된 최고 점수: " + PlayerPrefs.GetString("BestPlayerHP"));
        }
        else
        {
            Debug.Log("최고 점수 없음");
        }
    }

    public void OpenSkin02(string fileName, string name)
    {
        images[0].sprite = Resources.Load<Sprite>("Portrait/" + fileName);
        images[1].sprite = Resources.Load<Sprite>("Portrait/" + fileName + "_" + 2);
        images[2].sprite = Resources.Load<Sprite>("Portrait/" + fileName + "_" + 3);

        for (int i = 0; i < images.Length; i++)
        {
            names[i].text = name;
        }
    }
}
