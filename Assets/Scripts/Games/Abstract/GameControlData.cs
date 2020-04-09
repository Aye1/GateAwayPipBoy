using Mirror;

public abstract class GameControlData : NetworkBehaviour
{
    [SyncVar(hook = "OnValueChange")]
    protected NetworkIdentity MainGameNetworkIdentity;

    public GameData MainGameData { get; set; }

    public abstract GameType GetGameType();

    private bool clientStarted;
    private bool mainGameDataSet;

    public override void OnStartClient()
    {
        transform.SetParent(GameManager.Instance.transform);
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

    // Find Main Game Data with its identity
    // This method sets it for the server
    // For the client, it's set in OnValueChange
    public void SetMainGameIdentity(NetworkIdentity identity)
    {
        MainGameNetworkIdentity = identity;
        FindMainGameData();
    }

    private void FindMainGameData()
    {
        if (MainGameNetworkIdentity != null)
        {
            MainGameData = MainGameNetworkIdentity.GetComponent<GameData>();
            mainGameDataSet = true;
            CheckIsReady();
        }
    }

    // Sets the Main Game Data for the client
    public void OnValueChange(NetworkIdentity oldIdentity, NetworkIdentity newIdentity)
    {
        MainGameNetworkIdentity = newIdentity;
        FindMainGameData();
    }

    [Command]
    public void CmdExit(NetworkIdentity playerIdentity)
    {
        MainGameData.PlayerExit(playerIdentity);
        Destroy(gameObject);
    }
}
