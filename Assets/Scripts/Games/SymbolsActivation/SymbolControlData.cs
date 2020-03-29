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
        MainSymbolGameData.AddSymbol(symbol);
    }
}
