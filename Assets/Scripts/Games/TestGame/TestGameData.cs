

public class TestGameData : GameData
{
    public override void CreateControls()
    {
        GameControlData control = GameManager.Instance.CreateControlData(GameType.TestGame);
        control.SetMainGameIdentity(netIdentity);
        // TODO: select only specific players
        GameManager.Instance.SendControlBroadcast(control, CustomNetworkManager.Instance.ConnectedPlayers);
    }

    public override DisplayType GetDisplayType()
    {
        return DisplayType.TabletAndPhone;
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
