using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeGame : MonoBehaviour
{

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _winButton;
    [SerializeField] private Button _restartButton;
    public GameData gameData;

    // Start is called before the first frame update
    public void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _winButton.onClick.AddListener(WinGame);
        _restartButton.onClick.AddListener(ResetGame);
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
        gameData.status = GameStatus.Started;
    }

    public void WinGame()
    {
        gameData.status = GameStatus.Finished;
    }

    public void ResetGame()
    {
       gameData.status = GameStatus.NotStarted;
    }
}
