using UnityEngine;
using TMPro;

public class SymbolGameView : GameView
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI _answerText;
    [SerializeField] private TextMeshProUGUI _currentText;
    [SerializeField] private TextMeshProUGUI _victoryText;
    [SerializeField] private Color baseColor;
    [SerializeField] private Color winColor;
    [SerializeField] private Color failColor;
#pragma warning restore 0649

    private SymbolGameData SpecificGameData
    {
        get { return (SymbolGameData)base.GameData; }
    }

    private void Update()
    {
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        _answerText.text = SpecificGameData.clientPartialResult;
        _currentText.text = GetCurrentTextWithUnderscores();
    }

    private string GetCurrentTextWithUnderscores()
    {
        string text = SpecificGameData.currentText;
        int neededUnderscores = SpecificGameData.result.Length - text.Length;
        for(int i = 0; i < neededUnderscores; i++)
        {
            text += "_";
        }
        return text;
    }

    protected override void OnGameStatusChanged(GameStatus newStatus)
    {
        switch(newStatus)
        {
            case GameStatus.Won:
                _answerText.gameObject.SetActive(false);
                _currentText.gameObject.SetActive(false);
                _victoryText.gameObject.SetActive(true);
                break;
            case GameStatus.Failed:
                _currentText.color = failColor;
                break;
            case GameStatus.Started:
                _currentText.color = Color.black;
                break;
        }
    }
}
