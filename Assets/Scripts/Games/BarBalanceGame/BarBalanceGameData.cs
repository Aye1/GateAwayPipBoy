﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBalanceGameData : GameData
{
    public float currentValue;
    public float decreaseSpeed = 10;

    public override void CreateControls()
    {
        var barControls = GameManager.Instance.CreateControlData(GameType.BarBalanceGame) as BarBalanceControlData;

        barControls.SetMainGameIdentity(netIdentity);
        GameManager.Instance.SendControlBroadcast(barControls, playerIdentities);
    }

    public override DisplayType GetDisplayType()
    {
        return DisplayType.TabletAndPhone;
    }

    public override string GetGameName()
    {
        return "Bar balance game";
    }

    public override GameType GetGameType()
    {
        return GameType.BarBalanceGame;
    }

    public override void InitGame()
    {
        status = GameStatus.Started;
    }

    public void SetCurrentValue(float newValue)
    {
        currentValue = newValue;
    }

    public void Increment(float addedValue)
    {
        currentValue += addedValue;
    }

    public void Update()
    {
        currentValue = Math.Max(0, currentValue - Time.deltaTime * decreaseSpeed);
    }
}
