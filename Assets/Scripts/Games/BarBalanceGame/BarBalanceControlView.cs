using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBalanceControlView : GameControlView
{
    protected override void UpdateUI()
    {
        return;
    }

    public void OnPressedButton()
    {
        (ControlData as BarBalanceControlData).OnPress();
    }
}
