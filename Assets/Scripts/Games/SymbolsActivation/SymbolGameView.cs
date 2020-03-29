using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SymbolGameView : GameView
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI _answerText;
    [SerializeField] private TextMeshProUGUI _currentText;
    [SerializeField] private Transform _controlsHolder;
    [SerializeField] private Color baseColor;
    [SerializeField] private Color winColor;
    [SerializeField] private Color failColor;
#pragma warning restore 0649

    private SymbolGameData GameData
    {
        get { return (SymbolGameData)gameData; }
    }

    private void Update()
    {
        UpdateTexts();
        UpdateColor();
    }

    public Transform GetSymbolsControlHolder()
    {
        return _controlsHolder;
    }

    private void UpdateTexts()
    {
        _answerText.text = GameData.result;
        _currentText.text = GetCurrentTextWithUnderscores();
    }

    private string GetCurrentTextWithUnderscores()
    {
        string text = GameData.currentText;
        int neededUnderscores = GameData.result.Length - text.Length;
        for(int i = 0; i < neededUnderscores; i++)
        {
            text += "_";
        }
        return text;
    }

    private void UpdateColor()
    {
        Color currentColor = Color.black;
        switch(gameData.status)
        {
            case GameStatus.Failed:
                currentColor = failColor;
                break;
            case GameStatus.Won:
                currentColor = winColor;
                break;
        }
        _currentText.color = currentColor;
    }
}
