using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI name;
    public TextMeshProUGUI expText;


    private void Awake()
    {
        Debug.Log(name.text);
    }

    public void OnClicked()
    {
        string imageName = image.sprite.name;
        PortraitManager.instance.OpenSkin02(imageName, name.text);
    }
}