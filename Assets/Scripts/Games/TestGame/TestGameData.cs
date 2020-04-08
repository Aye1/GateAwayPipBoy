

public class TestGameData : GameData
{
    public override void CreateControls()
    {
        GameControlData control = GameManager.Instance.CreateControlData(GameType.TestGame);
        control.MainGameNetworkIdentity = netIdentity;
        // TODO: select only specific players
        GameManager.Instance.SendControlBroadcast(control, CustomNetworkManager.Instance.ConnectedPlayers);
    }

    public override string GetGameName()
    {
        return "Test Game";
    }

    public override GameType GetGameType()
    {
        return GameType.TestGame;
    }

    public override void InitGame()
    {
        return;
    }
}
