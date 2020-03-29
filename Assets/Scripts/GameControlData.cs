using Mirror;

public abstract class GameControlData : NetworkBehaviour
{
    public GameData MainGameData { get; private set; }

    public GameType GetGameType() { return MainGameData.GetGameType(); }

    public override void OnStartClient()
    {
        MainGameData = FindObjectOfType<GameData>();
        if (hasAuthority)
        {
            GamesViewsManager.Instance.CreateControlView(this);
        }
    }
}
