using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BarBalanceControlData : GameControlData
{
    [SyncVar]
    public int controlIdx;

    public override GameType GetGameType()
    {
        return GameType.BarBalanceGame;
    }

    public void OnPress()
    {
        var balanceGameData = MainGameData as BarBalanceGameData;
        balanceGameData.Increment(controlIdx, balanceGameData.increaseSpeeds[controlIdx]);
    }
}
