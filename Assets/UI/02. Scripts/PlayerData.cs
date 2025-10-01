using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int eatFruit;    // 상인 해금 조건
    public int playTime1;   // 귀족 (남) 해금 조건
    public int playTime2;   // 귀족 (여) 해금 조건
    public int sideTouch;   // 닌자 해금 조건
    public int killSlime;   // 대장장이 해금 조건
    public int turnStage;   // 농부 해금 조건
}