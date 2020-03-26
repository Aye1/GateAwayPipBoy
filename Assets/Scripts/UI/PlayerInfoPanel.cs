using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoPanel : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TMP_InputField _nameInputField;
#pragma warning restore 0649

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        _nameInputField.text = player.debugInfo;
        _nameInputField.onEndEdit.AddListener(OnInputFieldEndEdit);
    }

    private void OnInputFieldEndEdit(string text)
    {
        player.CmdSetDebugInfo(text);
    }
}
