using System;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map instance;
    public GameObject[] background;
    
    public float time = 0;
    public int _mapIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for(int i = 0; i < background.Length; i++)
            background[i].SetActive(false);
        background[0].SetActive(true);
    }

    public void OnBackground(float len = 0)
    {
        if (_mapIndex + 1 == background.Length)
        {
            _mapIndex++;
            return;
        }
        background[++_mapIndex].SetActive(true);
        background[_mapIndex].transform.position += new Vector3(0, len, 0);
        
        string bgmName = background[_mapIndex].name;
        SoundManager.instance.BgmPlay(bgmName);
        Debug.Log($"배경음 요청 : {bgmName}");
    }
}
