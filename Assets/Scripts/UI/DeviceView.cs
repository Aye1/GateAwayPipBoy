using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeviceView : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI _connectionIdText;
    [SerializeField] private TextMeshProUGUI _addressText;
    [SerializeField] private TextMeshProUGUI _debugInfo;
    [SerializeField] private GameInfoPanel _gameInfo;
#pragma warning restore 0649

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            _connectionIdText.text = "Connection " + player.Connection.connectionId;
            _addressText.text = player.Connection.address;
            _debugInfo.text = player.debugInfo;
        }
        else
        {
            _connectionIdText.text = "Player not found";
            _addressText.text = "N/A";
            _debugInfo.text = "N/A";
        }
    }

    public void SetCurrentGame(GameData gameData)
    {
        _gameInfo.gameData = gameData;
    }
}
