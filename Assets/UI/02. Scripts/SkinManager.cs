using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager instance;

    public SkinData[] portraits;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    // 스킨 보유 초기화
    public void InitData()
    {
        foreach (var portrait in portraits)
        {
            PlayerPrefs.SetInt(portrait.name, 0); // false
        }
        PlayerPrefs.Save();
    }

    // 스킨 보유 확인
    public void LoadData()
    {
        foreach (var portrait in portraits)
        {
            portrait.isHave = PlayerPrefs.GetInt(portrait.name, 0) == 1;
        }
    }

    // 스킨 보유 저장
    public void SaveData()
    {
        foreach (var portrait in portraits)
        {
            int i = portrait.isHave ? 1 : 0;
            PlayerPrefs.SetInt(portrait.name, i);
            PlayerPrefs.Save();

            Debug.Log($"portrait 1" + portrait.isHave);
            portrait.isHave = false;
            Debug.Log($"portrait 2" + portrait.isHave);
        }
    }

    public void AchievementCheak()
    {
        PlayerData PlayerData = PlayerManager.instance.PlayerData;
        
        # region 상인 스킨 조건문
        if (PlayerData.eatFruit >= 3)
        {
            portraits[0].isHave = true;
            if (PlayerData.eatFruit >= 10)
            {
                portraits[1].isHave = true;
                if (PlayerData.eatFruit >= 500)
                {
                    portraits[2].isHave = true;
                }
            }
        }
        # endregion
        # region 대장장이 스킨 조건문
        if (PlayerData.killSlime >= 3)
        {
            portraits[12].isHave = true;
            if (PlayerData.killSlime >= 10)
            {
                portraits[13].isHave = true;
                if (PlayerData.killSlime >= 500)
                {
                    portraits[14].isHave = true;
                }
            }
        }
        # endregion
        SaveData();
        PlayerManager.instance.SaveData();
    }
}
