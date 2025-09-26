using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
    public GameObject skinUI;
    public static PortraitManager Instance;
    public CharacterInfo[] _characterInfos;
    
    [SerializeField] private Image[] images;
    [SerializeField] private TextMeshProUGUI[] names;
    [SerializeField] private TextMeshProUGUI[] exps; // 설명

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }

    public void OnClicked(int index)
    {
        skinUI.SetActive(true);
        var idx = index * 3;
        // 0번 -> 0 1 2
        // 1번 -> 3 4 5
        // 2번 -> 6 7 8
        for (int i = 0; i < 3; i++)
        {
            images[i].sprite = _characterInfos[idx + i].img;
            names[i].text = _characterInfos[idx + i].name;
            exps[i].text = _characterInfos[idx + i].story;
        }
    }
}
