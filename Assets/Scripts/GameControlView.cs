using UnityEngine;
using Mirror;

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


    public void ExitGame()
    {
        ControlData.CmdExit(NetworkClient.connection.identity);
        Destroy(GetComponentInParent<GameView>().gameObject);
    }
}
