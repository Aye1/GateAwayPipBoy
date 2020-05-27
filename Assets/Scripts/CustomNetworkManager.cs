using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class CustomNetworkManager : NetworkManager
{
    #region Events
    public delegate void ClientConnectedToServer(NetworkConnection conn);
    public static ClientConnectedToServer OnClientConnectedToServer;

    public delegate void ClientDisconnectedFromServer(NetworkConnection conn);
    public static ClientDisconnectedFromServer OnClientDisconnectedFromServer;

    public delegate void ClientWillDisconnectFromServer(NetworkConnection conn);
    public static ClientWillDisconnectFromServer OnClientWillDisconnectFromServer;

    public delegate void ClientDisconnected();
    public static ClientDisconnected OnClientDisconnected;

    public delegate void PlayerAddedToServer(Player newPlayer);
    public static PlayerAddedToServer OnPlayerAddedToServer;

    public delegate void ServerStarted();
    public static ServerStarted OnServerStarted;
    #endregion

    public static CustomNetworkManager Instance { get; private set; }

    public List<Player> ConnectedPlayers { get; private set; }

    public DeviceType deviceType;
 
    public override void Awake()
    {
        base.Awake();
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ConnectedPlayers = new List<Player>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartClient(DeviceType type)
    {
        StartClient();
        deviceType = type;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        OnServerStarted?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        OnClientConnectedToServer?.Invoke(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        OnClientWillDisconnectFromServer?.Invoke(conn);
        base.OnServerDisconnect(conn);
        Player player = GetPlayer(conn);
        ConnectedPlayers.Remove(player);
        OnClientDisconnectedFromServer?.Invoke(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Debug.Log("Player added");
        GameObject createdPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player player = createdPlayer.GetComponent<Player>();
        player.Connection = conn;
        ConnectedPlayers.Add(player);
        player.playerName = "Player " + ConnectedPlayers.Count();
        NetworkServer.AddPlayerForConnection(conn, createdPlayer);
        OnPlayerAddedToServer?.Invoke(player);
    }

    // WARNING: this is never called at the moment, bug on the Mirror side
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        OnClientDisconnected?.Invoke();
    }

    public Player GetPlayer(NetworkConnection connection)
    {
        return ConnectedPlayers.First(x => x.Connection == connection);
    }

    public Player GetPlayer(NetworkIdentity identity)
    {
        return ConnectedPlayers.First(x => x.netIdentity == identity);
    }
}
