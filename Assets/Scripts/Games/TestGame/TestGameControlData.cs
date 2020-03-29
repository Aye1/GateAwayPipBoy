using Mirror;

public class TestGameControlData : GameControlData
{
    [Command]
    public void CmdSetStatus(GameStatus status)
    {
        MainGameData.SetStatus(status);
    }
}
