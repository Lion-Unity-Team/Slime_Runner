using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    // 데이터를 저장될 이름
    [SerializeField] PlayerData playerData;

    private Type PlayerType;
    
    public PlayerData PlayerData => playerData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        PlayerType = playerData.GetType();
    }
    

    // 플레이어 데이터 초기화 PlayerPrefs = 0
    public void InitData()
    {
        foreach (FieldInfo field in PlayerType.GetFields())
        {
            PlayerPrefs.SetInt(field.Name, 0);
        }
        PlayerPrefs.Save();
    }

    // 플레이어 데이터를 블러오는 함수 PlayerPrefs -> Scriptable Obj
    public void LoadData()
    {
        foreach (FieldInfo field in PlayerType.GetFields())
        {
            field.SetValue(playerData, PlayerPrefs.GetInt(field.Name)); 
        }
    }

    // 플레이어 데이터를 저장하는 함수 Scriptable Obj -> PlayerPrefs
    public void SaveData()
    {
        PlayerType = playerData.GetType();
        foreach (FieldInfo field in PlayerType.GetFields())
        {
            PlayerPrefs.SetInt(field.Name,(int)field.GetValue(playerData));
        }
        PlayerPrefs.Save();
    }
}
