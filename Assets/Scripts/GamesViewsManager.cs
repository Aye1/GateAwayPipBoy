using UnityEngine;

public class GamesViewsManager : MonoBehaviour
{
    public Transform gamesHolder;
    public static GamesViewsManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
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
        createdGame.gameData = data;
        createdGame.transform.localPosition = Vector3.zero;
    }

    public void CreateControlView(GameControlData data)
    {
        GameType type = data.GetGameType();
        GameBinding binding = GameManager.Instance.GetGame(type);
        //TODO: is there a better way to pass the main view?
        Transform holder = FindObjectOfType<GameView>().controlsHolder;
        GameControlView view = Instantiate(binding.gameControlView, Vector3.zero, Quaternion.identity, holder);
        view.ControlData = data;
        view.transform.localPosition = Vector3.zero;
    }
}
