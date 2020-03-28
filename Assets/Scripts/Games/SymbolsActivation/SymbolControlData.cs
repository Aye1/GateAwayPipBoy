using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SymbolControlData : NetworkBehaviour
{
    [SyncVar]
    public char symbol;

    public SymbolGameData mainGameData;

    public override void OnStartClient()
    {
        if(hasAuthority)
        {
            GamesProvider.Instance.CreateSymbolControl(this);
        }
    }

    [Command]
    public void CmdSendSymbol()
    {
        mainGameData.AddSymbol(symbol);
    }
}
