using Mirror;

public enum MazeControlDirection { Left, Right, Up, Down};
public class MazeGameControlData : GameControlData
{
    [SyncVar]
    public MazeControlDirection direction;

    public MazeGameData MainMazeData
    {
        get { return (MazeGameData)MainGameData; }
    }

    public override GameType GetGameType()
    {
        return GameType.MazeGame;
    }

    public string GetSymbol()
    {
        string symbol = "";
        switch(direction)
        {
            case MazeControlDirection.Left:
                symbol = "<";
                break;
            case MazeControlDirection.Right:
                symbol = ">";
                break;
            case MazeControlDirection.Up:
                symbol = "^";
                break;
            case MazeControlDirection.Down:
                symbol = "v";
                break;
        }
        return symbol;
    }

    [Command]
    public void CmdSendInput()
    {
        MainMazeData.SendInput(direction);
    }
}
