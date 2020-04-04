using Mirror;

public abstract class GameControlData : NetworkBehaviour
{
    [SyncVar(hook = "OnValueChange")]
    public NetworkIdentity MainGameNetworkIdentity;

    public GameData MainGameData { get; set; }

    public abstract GameType GetGameType();

    private bool clientStarted;
    private bool mainGameDataSet;

    public override void OnStartClient()
    {
        if(hasAuthority)
        {
            clientStarted = true;
            CheckIsReady();
        }
    }

    private void CheckIsReady()
    {
        if(clientStarted && mainGameDataSet)
        {
            CreateView();
        }
    }

    private void CreateView()
    {
        if(hasAuthority)
        {
            GamesViewsManager.Instance.CreateControlView(this);
        }
    }

    public void OnValueChange(NetworkIdentity oldIdentity, NetworkIdentity newIdentity)
    {
        MainGameNetworkIdentity = newIdentity;
        if (MainGameNetworkIdentity != null)
        {
            MainGameData = MainGameNetworkIdentity.GetComponent<GameData>();
            mainGameDataSet = true;
            CheckIsReady();
        }
    }

    [Command]
    public void CmdExit(NetworkIdentity playerIdentity)
    {
        MainGameData.PlayerExit(playerIdentity);
        Destroy(gameObject);
    }
}
