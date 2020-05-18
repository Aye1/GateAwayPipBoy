using UnityEngine;
using Mirror;
using System.Linq;
using System.Collections;

public enum GameStatus { NotStarted, Started, Won, Failed };
public enum GameType { TestGame, SymbolGame, MazeGame, BarBalanceGame };
public enum DisplayType { Unknown, TabletOnly, PhoneOnly, TabletAndPhone };

public abstract class GameData : NetworkBehaviour
{
    #region Sync Vars
    [SyncVar(hook = "StatusSynced")]
    public GameStatus status = GameStatus.NotStarted;
    [SyncVar]
    public string gameName;

    [System.Serializable]
    public class SyncNIList : SyncList<NetworkIdentity> { }
    public SyncNIList playerIdentities = new SyncNIList();
    #endregion

    #region Events
    public delegate void GameExit();
    public GameExit OnGameExit;

    public delegate void PlayerExitsGame(GameData data, NetworkIdentity player);
    public PlayerExitsGame OnPlayerExitsGame;

    public delegate void StatusChanged(GameStatus newStatus);
    public StatusChanged OnStatusChanged;
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
        CreateControls();
    }

    public override void OnStartClient()
    {
        transform.SetParent(GameManager.Instance.transform);
        Player associatedPlayer = MirrorHelpers.GetClientLocalPlayer(netIdentity);
        if(TypesHelpers.HasMatchingType(GetDisplayType(), associatedPlayer.playerType) && IsLocalPlayerInGame())
        {
            GamesViewsManager.Instance.CreateGameView(this);
        }
    }

    public void SetStatus(GameStatus status)
    {
        this.status = status;
        OnStatusChanged?.Invoke(status);
        if(status == GameStatus.Won)
        {
            StartCoroutine(DestroyGameAfterTime(5));
        }
    }

    public void StatusSynced(GameStatus oldStatus, GameStatus newStatus)
    {
        SetStatus(newStatus);
    }

    public void PlayerExit(NetworkIdentity identity)
    {
        RemovePlayer(identity);
        OnPlayerExitsGame?.Invoke(this, identity);
    }

    private void ExitGame()
    {
        OnGameExit?.Invoke();
        GameManager.Instance.ClearAllGameData();
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
            StartCoroutine(DestroyGameAfterTime(1));
        }
    }

    public bool IsLocalPlayerInGame()
    {
        return playerIdentities.Contains(MirrorHelpers.GetClientLocalPlayerIdentity(netIdentity));
    }

    private NetworkIdentity GetPlayerWithConnection(NetworkConnection conn)
    {
        return playerIdentities.FirstOrDefault(x => x.connectionToClient == conn);
    }

    protected IEnumerator DestroyGameAfterTime(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        ExitGame();
    }
}
