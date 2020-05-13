using UnityEngine;

public class GamesViewsManager : MonoBehaviour
{
    public Transform gamesHolder;
    public Transform controlsHolder;
    public static GamesViewsManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            CustomNetworkManager.OnClientDisconnected += ClearAllViews;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void CreateGameView(GameData data)
    {
        GameType type = data.GetGameType();
        GameBinding binding = GameManager.Instance.GetGame(type);
        GameView createdGame = Instantiate(binding.gameView, Vector3.zero, Quaternion.identity, gamesHolder);
        createdGame.GameData = data;
        createdGame.transform.localPosition = Vector3.zero;
    }

    public void CreateControlView(GameControlData data)
    {
        GameType type = data.GetGameType();
        GameBinding binding = GameManager.Instance.GetGame(type);
        GameControlView view = Instantiate(binding.gameControlView, Vector3.zero, Quaternion.identity, controlsHolder);
        view.ControlData = data;
        view.transform.localPosition = Vector3.zero;
    }

    public void ClearAllViews()
    {
        foreach(Transform t in gamesHolder)
        {
            Destroy(t.gameObject);
        }
        foreach(Transform t in controlsHolder)
        {
            Destroy(t.gameObject);
        }
    }
}
