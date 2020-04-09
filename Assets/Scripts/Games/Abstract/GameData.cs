using UnityEngine;
using Mirror;
using System.Linq;

public enum GameStatus { NotStarted, Started, Won, Failed };
public enum GameType { TestGame, SymbolGame, MazeGame };
public enum DisplayType { Unknown, TabletOnly, PhoneOnly, TabletAndPhone };

public abstract class GameData : NetworkBehaviour
{
    #region Sync Vars
    [SyncVar]
    public GameStatus status = GameStatus.NotStarted;
    [SyncVar]
    public string gameName;

    [System.Serializable]
    public class SyncNIList : SyncList<NetworkIdentity> { }
    public SyncNIList playerIdentities = new SyncNIList();
    #endregion

    #region Events
    public delegate void GameExit(GameData data);
    public GameExit OnGameExit;

    public delegate void PlayerExitsGame(GameData data, NetworkIdentity player);
    public PlayerExitsGame OnPlayerExitsGame;
    #endregion

    public abstract void InitGame();
    public abstract GameType GetGameType();
    public abstract string GetGameName();
    public abstract void CreateControls();
    public abstract DisplayType GetDisplayType();

    private void Awake()
    {
        gameName = GetGameName();
        InitGame();
        CustomNetworkManager.OnClientWillDisconnectFromServer += CheckIfPlayerWasPlayingThisGame;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        CreateControls();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Player associatedPlayer = MirrorHelpers.GetClientLocalPlayer(netIdentity);
        if(TypesHelpers.HasMatchingType(GetDisplayType(), associatedPlayer.playerType))
        {
            GamesViewsManager.Instance.CreateGameView(this);
        }
    }

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
