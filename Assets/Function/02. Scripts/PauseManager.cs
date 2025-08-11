using NUnit.Framework.Constraints;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public Slime_Movement playerMovement;

    public void OpenSettings()
    {
        playerMovement.canMove = false;
        Ground.canMoving = false;
        Time.timeScale = 0;
    }

    public void CloseSettings()
    {
        playerMovement.canMove = true;
        Ground.canMoving = true;
        Time.timeScale = 1;
    }
}
