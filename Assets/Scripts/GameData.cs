using UnityEngine;
using Mirror;

public enum GameStatus { NotStarted, Started, Won, Failed };
public enum GameType { TestGame, SymbolGame };

public abstract class GameData : NetworkBehaviour
{
    [SyncVar]
    public GameStatus status = GameStatus.NotStarted;
    [SyncVar]
    public string gameName;

    public class SyncNIList : SyncList<NetworkIdentity> { }
    public SyncNIList playerIdentities;

    public delegate void GameExit(GameData data);
    public GameExit OnGameExit;

    public abstract void InitGame();
    public abstract GameType GetGameType();
    public abstract string GetGameName();

    private void Awake()
    {
        playerIdentities = new SyncNIList();
        InitGame();
        gameName = GetGameName();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("Starting game on client");
        if(netIdentity.isClient)
        {
            GamesViewsManager.Instance.CreateGameView(this);
        }
    }

    // Only the server can control the data
    // We force it to run the status update with [Command]
    public void SetStatus(GameStatus status)
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
