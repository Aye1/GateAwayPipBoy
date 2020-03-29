

public class TestGameData : GameData
{
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
       
    }
}
