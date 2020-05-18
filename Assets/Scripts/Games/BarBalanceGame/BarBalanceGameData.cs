using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBalanceGameData : GameData
{
    public int nbBars = 4;
    public float[] currentValues = { 70f, 80f, 70f, 70f };
    public int[] currentGoalMins = { 30, 40, 50, 60 };
    public int[] currentGoalMaxs= { 50, 60, 70, 80 };
    public float[] decreaseSpeeds = { 5f, 5f, 5f, 5f };
    public float[] increaseSpeeds = { 15f, 10f, 20f, 10f };

    private int maxValue = 100;

    public override void CreateControls()
    {
        var barControls = new List<BarBalanceControlData>();
        for (int i = 0; i < nbBars; ++i)
        {
            var controlData = GameManager.Instance.CreateControlData(GameType.BarBalanceGame) as BarBalanceControlData;
            controlData.controlIdx = i;
            controlData.SetMainGameIdentity(netIdentity);
            barControls.Add(controlData);
        }

      
        GameManager.Instance.SendControlsBroadcast(barControls, playerIdentities);
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
        SetStatus(GameStatus.Started);
    }

    public void SetCurrentValue(int barIdx, float newValue)
    {
        currentValues[barIdx] = Math.Min(100, Math.Max(0, newValue));
    }

    public void Increment(int barIdx, float addedValue)
    {
        currentValues[barIdx] = Math.Min(100, currentValues[barIdx] + addedValue);
    }

    public void Update()
    {
        for (int i=0; i<nbBars; ++i)
            currentValues[i] = Math.Max(0, currentValues[i] - Time.deltaTime * decreaseSpeeds[i]);
    }
}
