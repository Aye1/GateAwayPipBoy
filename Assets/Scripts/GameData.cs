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

    public class SyncNIList : SyncList<NetworkIdentity> { }
    public SyncNIList playerIdentities;

    public delegate void GameExit(GameData data);
    public GameExit OnGameExit;

    public virtual void InitGame() { }

    private void Awake()
    {
        playerIdentities = new SyncNIList();
        InitGame();
    }

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

    [Command]
    public void CmdExit()
    {
        OnGameExit?.Invoke(this);
    }

    [Command]
    public void CmdAddPlayer(NetworkIdentity playerIdentity)
    {
        if(!playerIdentities.Contains(playerIdentity))
        {
            playerIdentities.Add(playerIdentity);
        }
    }

    [Command]
    public void CmdRemovePlayer(NetworkIdentity playerIdentity)
    {
        playerIdentities.Remove(playerIdentity);
    }
}
