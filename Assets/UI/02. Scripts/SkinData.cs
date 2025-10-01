using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SkinData : ScriptableObject
{
    public int index;
    
    public Sprite ImageFile;
    public string jobName;

    public bool isHave;
    
    public string story1;
    public string story2;
}