using Mirror;

public abstract class GameControlData : NetworkBehaviour
{
    public GameData MainGameData { get; private set; }

    public GameType GetGameType() { return MainGameData.GetGameType(); }

    public override void OnStartClient()
    {
        // TODO: can we pass the main data differently?
        MainGameData = FindObjectOfType<GameData>();
        if (hasAuthority)
        {
            GamesViewsManager.Instance.CreateControlView(this);
        }
    }

    [Command]
    public void CmdExit()
    {
        MainGameData.PlayerExit(NetworkClient.connection.identity);
        Destroy(gameObject);
    }
}
