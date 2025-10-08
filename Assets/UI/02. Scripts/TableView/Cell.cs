using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Cell : MonoBehaviour
{
    [SerializeField] private Button cellButton;
    [SerializeField] private int FileIndex;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text subtitleText;

    public int CellIndex { get; private set; }

    public void SetItem(Portrait portrait, int index, bool isHave)
    {
        FileIndex = portrait.index;
        image.sprite = Resources.Load<Sprite>("Portrait/"+portrait.imageFileName);
        titleText.text = portrait.jobName;
        subtitleText.text = portrait.subtitle;

        if (isHave)
        {
            cellButton.interactable = true;
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            cellButton.interactable = false;
            image.color = new Color(1, 1, 1, 0.5f);
        }
        
        CellIndex = index;
    }

    public void OnClick()
    {
        ScrollViewController.instance.SecondLoadData(FileIndex -1);
    }
}