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
            
            portrait.isHave = false;
        }
    }

    // 플레이어의 데이터를 확인하고 스킨 해금을 관리하는 함수
    public void AchievementCheak()
    {
        PlayerData PlayerData = PlayerManager.instance.PlayerData;
        
        
        # region 상인 스킨 조건문
        if (PlayerData.eatFruit >= 36)
        {
            portraits[0].isHave = true;
            if (PlayerData.eatFruit >= 100)
            {
                portraits[1].isHave = true;
                if (PlayerData.eatFruit >= 300)
                {
                    portraits[2].isHave = true;
                }
            }
        }
        # endregion
        # region 귀족 (남) 스킨 조건문
        if (PlayerData.playTime1 >= 180)
        {
            portraits[3].isHave = true;
            if (PlayerData.playTime1 >= 600)
            {
                portraits[4].isHave = true;
                if (PlayerData.playTime1 >= 1200)
                {
                    portraits[5].isHave = true;
                }
            }
        }
        # endregion
        # region 귀족 (여) 스킨 조건문
        if (PlayerData.playTime2 >= 180)
        {
            portraits[6].isHave = true;
            if (PlayerData.playTime2 >= 600)
            {
                portraits[7].isHave = true;
                if (PlayerData.playTime2 >= 1200)
                {
                    portraits[8].isHave = true;
                }
            }
        }
        # endregion
        # region 도둑 스킨 조건문
        if (PlayerData.sideTouch >= 180)
        {
            portraits[9].isHave = true;
            if (PlayerData.sideTouch >= 1800)
            {
                portraits[10].isHave = true;
                if (PlayerData.sideTouch >= 10000)
                {
                    portraits[11].isHave = true;
                }
            }
        }
        # endregion
        # region 대장장이 스킨 조건문
        if (PlayerData.killSlime >= 100)
        {
            portraits[12].isHave = true;
            if (PlayerData.killSlime >= 300)
            {
                portraits[13].isHave = true;
                if (PlayerData.killSlime >= 1000)
                {
                    portraits[14].isHave = true;
                }
            }
        }
        # endregion
        # region 농부 스킨 조건문
        if (PlayerData.turnStage >= 10)
        {
            portraits[15].isHave = true;
            if (PlayerData.turnStage >= 30)
            {
                portraits[16].isHave = true;
                if (PlayerData.turnStage >= 80)
                {
                    portraits[17].isHave = true;
                }
            }
        }
        # endregion
        
        SaveData();
        PlayerManager.instance.SaveData();
    }
}
