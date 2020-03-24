using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class Device : MonoBehaviour
{
    public NetworkConnection Connection { get; set; }

    [SerializeField] private TextMeshProUGUI _connectionIdText;
    [SerializeField] private TextMeshProUGUI _addressText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _connectionIdText.text = "Connection " + Connection.connectionId;
        _addressText.text = Connection.address;
    }
}
