using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SymbolControlView : GameControlView
{
#pragma warning disable 0649
    [SerializeField] private Button _symbolButton;
#pragma warning restore 0649

    public SymbolControlData SymbolData
    {
        get { return ((SymbolControlData)ControlData); }
    }

    private void Start()
    {
        _symbolButton.onClick.AddListener(SendSymbol);
    }

    private void SendSymbol()
    {
        SymbolData.CmdSendSymbol();
    }

    protected override void UpdateUI()
    {
        _symbolButton.interactable = true;
        _symbolButton.GetComponentInChildren<TextMeshProUGUI>().text = SymbolData.symbol.ToString();
    }
}
