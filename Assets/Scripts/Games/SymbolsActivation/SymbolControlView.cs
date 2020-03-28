using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SymbolControlView : MonoBehaviour
{

    private SymbolControlData _symbolData;
    public SymbolControlData SymbolData
    {
        get
        {
            return _symbolData;
        }
        set
        {
            if (value != _symbolData)
            {
                _symbolData = value;
                UpdateSymbolUI();
            }
        }
    }

#pragma warning disable 0649
    [SerializeField] private Button _symbolButton;
#pragma warning restore 0649


    private void UpdateSymbolUI()
    {
        _symbolButton.interactable = true;
        _symbolButton.GetComponentInChildren<TextMeshProUGUI>().text = SymbolData.symbol.ToString();
    }

    private void Start()
    {
        _symbolButton.onClick.AddListener(SendSymbol);
    }

    private void SendSymbol()
    {
        SymbolData.CmdSendSymbol();
    }
}
