using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int index;
    public Image image;
    public TextMeshProUGUI name;
    public TextMeshProUGUI expText;
    
    
    public string imageName;
    public string portraitName;
    public string exp;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PortraitManager.Instance.OnClicked(index);
        });
    }
}