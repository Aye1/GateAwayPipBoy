using Mirror;

public class SymbolControlData : GameControlData
{
    [SyncVar]
    public char symbol;
    
    public SymbolGameData MainSymbolGameData
    {
        get { return ((SymbolGameData) MainGameData); }
    }

    [Command]
    public void CmdSendSymbol()
    {
        // TODO: this does not work in server only mode
        MainSymbolGameData.AddSymbol(symbol);
    }

    public override GameType GetGameType()
    {
        return GameType.SymbolGame;
    }
}
