using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public delegate void ClientConnectedToServer(NetworkConnection conn);
    public ClientConnectedToServer OnClientConnectedToServer;

    public delegate void ClientDisconnectedFromServer(NetworkConnection conn);
    public ClientDisconnectedFromServer OnClientDisconnectedFromServer;

    public static CustomNetworkManager Instance { get; private set; }

    public override void Awake()
    {
        base.Awake();
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        OnClientConnectedToServer?.Invoke(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        OnClientDisconnectedFromServer?.Invoke(conn);
    }
}
