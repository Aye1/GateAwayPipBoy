using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestGameControlView : GameControlView
{
#pragma warning disable 0649
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _winButton;
    [SerializeField] private Button _loseButton;
    [SerializeField] private Button _restartButton;
    //[SerializeField] private Button _closeButton;
#pragma warning restore 0649

    private TestGameControlData TestControlData
    {
        get
        {
            return (TestGameControlData) ControlData;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _winButton.onClick.AddListener(WinGame);
        _loseButton.onClick.AddListener(LoseGame);
        _restartButton.onClick.AddListener(ResetGame);
        //_closeButton.onClick.AddListener(ExitGame);
    }

    public void Update()
    {
        UpdateUI();
    }

    protected override void UpdateUI()
    {
        if (TestControlData.MainGameData != null)
        {
            _startButton.interactable = TestControlData.MainGameData.status == GameStatus.NotStarted;
            _winButton.interactable = TestControlData.MainGameData.status == GameStatus.Started;
            _loseButton.interactable = TestControlData.MainGameData.status == GameStatus.Started;
            _restartButton.interactable = TestControlData.MainGameData.status != GameStatus.NotStarted;
        }
        else
        {
            _startButton.interactable = false;
            _winButton.interactable = false;
            _loseButton.interactable = false;
            _restartButton.interactable = false;
        }
    }

    public void StartGame()
    {
        TestControlData.CmdSetStatus(GameStatus.Started);
    }

    public void WinGame()
    {
        TestControlData.CmdSetStatus(GameStatus.Won);
    }

    public void LoseGame()
    {
        TestControlData.CmdSetStatus(GameStatus.Failed);
    }

    public void ResetGame()
    {
        TestControlData.CmdSetStatus(GameStatus.NotStarted);
    }

    /*public void ExitGame()
    {
        gameData.CmdExit();
        Destroy(gameObject);
    }*/
}
