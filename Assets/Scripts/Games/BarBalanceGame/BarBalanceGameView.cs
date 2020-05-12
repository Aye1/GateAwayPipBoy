using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBalanceGameView : GameView
{
    public MovingBalanceBar bar;


    public void Update()
    {
        bar.currentHeight = (gameData as BarBalanceGameData).currentValue;
    }
}
