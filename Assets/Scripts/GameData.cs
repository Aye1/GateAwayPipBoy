using UnityEngine;
using Mirror;
using System.Linq;

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

    public delegate void PlayerExitsGame(GameData data, NetworkIdentity player);
    public PlayerExitsGame OnPlayerExitsGame;

    public abstract void InitGame();
    public abstract GameType GetGameType();
    public abstract string GetGameName();

    private void Awake()
    {
        playerIdentities = new SyncNIList();
        InitGame();
        gameName = GetGameName();
        CustomNetworkManager.OnClientDisconnectedFromServer += CheckIfPlayerWasPlayingThisGame;
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

    public void PlayerExit(NetworkIdentity identity)
    {
        RemovePlayer(identity);
        OnPlayerExitsGame?.Invoke(this, identity);
    }

    public void ExitGame()
    {
        OnGameExit?.Invoke(this);
    }

    public void DestroyGame()
    {
        Destroy(gameObject);
    }

    public void CheckIfPlayerWasPlayingThisGame(NetworkConnection conn)
    {
        if(GetPlayerWithConnection(conn) != null)
        {
            RemovePlayer(conn.identity);
        }
    }

    public void AddPlayer(NetworkIdentity playerIdentity)
    {
        if(!playerIdentities.Contains(playerIdentity))
        {
            playerIdentities.Add(playerIdentity);
        }
    }

    public void RemovePlayer(NetworkIdentity playerIdentity)
    {
        playerIdentities.Remove(playerIdentity);
        if (playerIdentities.Count == 0)
        {
            ExitGame();
        }
    }

    private NetworkIdentity GetPlayerWithConnection(NetworkConnection conn)
    {
        return playerIdentities.FirstOrDefault(x => x.connectionToClient == conn);
    }
}
