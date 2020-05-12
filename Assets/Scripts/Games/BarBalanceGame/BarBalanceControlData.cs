using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBalanceControlData : GameControlData
{
    public override GameType GetGameType()
    {
        return GameType.BarBalanceGame;
    }

    public void OnPress()
    {
        (MainGameData as BarBalanceGameData).Increment(15f);
    }
}
