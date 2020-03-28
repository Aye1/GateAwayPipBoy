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
#pragma warning restore 0649

    private SymbolGameData GameData
    {
        get { return (SymbolGameData)gameData; }
    }

    private void Update()
    {
        _answerText.text = GameData.result;
        _currentText.text = GameData.currentText;
    }

    public Transform GetSymbolsControlHolder()
    {
        return _controlsHolder;
    }
}
