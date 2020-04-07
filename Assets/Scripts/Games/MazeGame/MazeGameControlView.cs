using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MazeGameControlView : GameControlView
{
#pragma warning disable 0649
    private Button _button;
#pragma warning restore 0649

    private bool IsButtonPressed;

    public MazeGameControlData MazeControlData
    {
        get { return (MazeGameControlData) ControlData; }
    }

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        //_button.onClick.AddListener(SendInput);
    }

    private void Update()
    {
        if (IsButtonPressed)
            SendInput();
    }

    protected override void UpdateUI()
    {
        _button.interactable = true;
        _button.GetComponentInChildren<TextMeshProUGUI>().text = MazeControlData.GetSymbol();
    }

    public void SendInput()
    {
        MazeControlData.CmdSendInput();
    }

    public void OnPointerDown()
    {
        IsButtonPressed = true;
    }

    public void OnPointerUp()
    {
        IsButtonPressed = false;
    }
}
