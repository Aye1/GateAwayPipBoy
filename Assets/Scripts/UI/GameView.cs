using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    public GameData gameData;
    public Transform controlsHolder;

    public void Exit()
    {
        gameData.PlayerExit();
    }
}
