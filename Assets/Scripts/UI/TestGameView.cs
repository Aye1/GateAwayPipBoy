using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/* This is a View (+ Controller maybe) class
 * There should not be any reference to Mirror or network here
 * All data should be controlled by the GameData object
 * The GameData is a NetworkBehaviour object
 * It is controlled by the server, not the client
 */
public class TestGameView : GameView
{
#pragma warning disable 0649
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _winButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _closeButton;
#pragma warning restore 0649

    // Start is called before the first frame update
    public void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _winButton.onClick.AddListener(WinGame);
        _restartButton.onClick.AddListener(ResetGame);
        _closeButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameData != null)
        {
            _startButton.interactable = gameData.status == GameStatus.NotStarted;
            _winButton.interactable = gameData.status == GameStatus.Started;
            _restartButton.interactable = gameData.status != GameStatus.NotStarted;
        }
        else
        {
            _startButton.interactable = false;
            _winButton.interactable = false;
            _restartButton.interactable = false;
        }
    }

    public void StartGame()
    {
        gameData.CmdSetStatus(GameStatus.Started);
    }

    public void WinGame()
    {
        gameData.CmdSetStatus(GameStatus.Finished);
    }

    public void ResetGame()
    {
       gameData.CmdSetStatus(GameStatus.NotStarted);
    }

    public void ExitGame()
    {
        gameData.CmdExit();
        Destroy(gameObject);
    }
}
