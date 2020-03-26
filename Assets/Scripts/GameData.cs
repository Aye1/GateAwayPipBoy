using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum GameStatus { NotStarted, Started, Finished };

public class GameData : NetworkBehaviour
{
    [SyncVar]
    public GameStatus status;
    [SyncVar]
    public string gameName;

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("Starting game on client");
        if(netIdentity.isClient)
        {
            GamesProvider.Instance.CreateGame(this);
        }
    }

    // Only the server can control the data
    // We force it to run the status update with [Command]
    [Command]
    public void CmdSetStatus(GameStatus status)
    {
        this.status = status;
    }
}
