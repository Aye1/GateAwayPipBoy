using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using System;

public class PlayerInfoPanel : NetworkBehaviour
{
    [SerializeField] private TMP_InputField _nameInputField;
    public Player currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //CustomNetworkManager.OnPlayerAddedToServer += OnPlayerAddedToServer;
        _nameInputField.onEndEdit.AddListener(OnInputFieldEndEdit);
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*private void OnPlayerAddedToServer(NetworkConnection conn, Player player)
    {
        if(conn.identity == netIdentity)
        {
            currentPlayer = player;
        }
    }*/

    private void OnInputFieldEndEdit(string text)
    {
        currentPlayer.debugInfo = text;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //currentPlayer = DevicesMonitor.Instance.GetPlayerWithNetworkIdentity(netIdentity);
        Debug.Log("player info panel onstartlocalplayer");
    }
}
