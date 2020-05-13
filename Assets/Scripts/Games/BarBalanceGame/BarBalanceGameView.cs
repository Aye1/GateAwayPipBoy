using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBalanceGameView : GameView
{
    public MovingBalanceBar bar;


    public void Update()
    {
        bar.currentHeight = (GameData as BarBalanceGameData).currentValue;
    }

    protected override void OnGameStatusChanged(GameStatus newStatus)
    {
        //TODO
    }
}
