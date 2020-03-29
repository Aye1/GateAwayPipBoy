using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameControlView : MonoBehaviour
{
    private GameControlData _controlData;
    public GameControlData ControlData
    {
        get
        {
            return _controlData;
        }
        set
        {
            if(_controlData != value)
            {
                _controlData = value;
                UpdateUI();
            }
        }
    }

    protected abstract void UpdateUI();
}
