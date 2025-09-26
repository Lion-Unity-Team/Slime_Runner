using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
    
    [SerializeField] private Image[] images;
    [SerializeField] private TextMeshProUGUI[] names;
    [SerializeField] private TextMeshProUGUI[] exps; // 설명

    public void OnClicked(string name)
    {
        images[0].sprite = Resources.Load<Sprite>("Portrait/" + name);
        for (int i = 1; i < images.Length; i++)
        {
            images[i].sprite = Resources.Load<Sprite>("Portrait/" + name + "_" + i);
        }
    }
}
