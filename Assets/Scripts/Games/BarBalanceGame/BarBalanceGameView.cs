using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBalanceGameView : GameView
{
    public MovingBalanceBar fullBar;


    private List<MovingBalanceBar> bars = new List<MovingBalanceBar>();

    public void Start()
    {
        var balanceGameData = GameData as BarBalanceGameData;
        for (int i=0; i < balanceGameData.nbBars; ++i)
        {
            bars.Add(Instantiate(fullBar, Vector3.zero, Quaternion.identity, transform));
            bars[i].transform.localPosition = Vector3.zero;
            // Todo : fix problem with "Z" transform position being random shit
            bars[i].GoalMin = balanceGameData.currentGoalMins[i];
            bars[i].GoalMax = balanceGameData.currentGoalMaxs[i];
        }
    }

    public void Update()
    {
        var balanceGameData = GameData as BarBalanceGameData;
        for (int i=0; i<balanceGameData.nbBars; ++i)
            bars[i].currentHeight = balanceGameData.currentValues[i];
    }

    protected override void OnGameStatusChanged(GameStatus newStatus)
    {
        //TODO
    }
}
