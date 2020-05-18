using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBalanceControlView : GameControlView
{
    public Text buttonText;

    protected override void UpdateUI()
    {
        return;
    }

    public void Start()
    {
        buttonText.text = "Barre " + (ControlData as BarBalanceControlData).controlIdx.ToString();
    }

    public void onPress()
    {
        (ControlData as BarBalanceControlData).OnPress();
    }
}
