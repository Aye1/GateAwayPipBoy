

public class TestGameData : GameData
{
    public override void CreateControls()
    {
        GameControlData control = GameManager.Instance.CreateControlData(GameType.TestGame);
        control.SetMainGameIdentity(netIdentity);
        GameManager.Instance.SendControlBroadcast(control, playerIdentities);
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
