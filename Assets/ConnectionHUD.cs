using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class ConnectionHUD : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject _connectedLayout;
    [SerializeField] private GameObject _disconnectedLayout;
    [SerializeField] private TMP_InputField _serverAddress;
    [SerializeField] private TextMeshProUGUI _connectionInfoText;
#pragma warning restore 0649

    // Start is called before the first frame update
    void Start()
    {
        _serverAddress.text = CustomNetworkManager.Instance.networkAddress;
    }

    // Update is called once per frame
    void Update()
    {
        bool isConnected = NetworkServer.active || NetworkClient.isConnected;
        _disconnectedLayout.SetActive(!isConnected);
        _connectedLayout.SetActive(isConnected);
        if(isConnected)
        {
            _connectionInfoText.text = (NetworkServer.active ? "Server - " : "Client - ") + _serverAddress.text;
        }
    }

    public void StartServer()
    {
        CustomNetworkManager.Instance.StartServer();
    }

    public void StartHost()
    {
        CustomNetworkManager.Instance.StartHost();
    }

    public void StartClient(PlayerType type)
    {
        CustomNetworkManager.Instance.StartClient(type);
        CustomNetworkManager.Instance.networkAddress = _serverAddress.text;
    }

    public void StartTabletClient()
    {
        StartClient(PlayerType.Tablet);
    }

    public void StartPhoneClient()
    {
        StartClient(PlayerType.Phone);
    }

    public void Disconnect()
    {
        CustomNetworkManager.Instance.StopHost();
    }
}
