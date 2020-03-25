using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class Device : MonoBehaviour
{
    //public NetworkConnection Connection { get; set; }

    [SerializeField] private TextMeshProUGUI _connectionIdText;
    [SerializeField] private TextMeshProUGUI _addressText;
    [SerializeField] private TextMeshProUGUI _debugInfo;

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
        /*else
        {
            _connectionIdText.text = "Connection " + Connection.connectionId;
            _addressText.text = Connection.address;
            _debugInfo.text = "No player associated";
        }*/
    }
}
