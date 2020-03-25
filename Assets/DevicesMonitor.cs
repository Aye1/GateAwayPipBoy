using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class DevicesMonitor : GameMasterBehaviour
{
    [SerializeField] private Device _deviceTemplate;

    public static DevicesMonitor Instance { get; private set; }

    public List<Device> ConnectedDevices { get; private set; }

    private void Awake()
    {
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

    protected override void Init()
    {
        ConnectedDevices = new List<Device>();
        //CustomNetworkManager.OnClientConnectedToServer += AddClient;
        CustomNetworkManager.OnClientDisconnectedFromServer += RemoveClient;
        CustomNetworkManager.OnPlayerAddedToServer += AddPlayer;
    }


    /*private void AddClient(NetworkConnection conn)
    {
        Device createdDevice = Instantiate(_deviceTemplate, Vector3.zero, Quaternion.identity, transform);
        createdDevice.Connection = conn;
        ConnectedDevices.Add(createdDevice);
    }*/

    private void RemoveClient(NetworkConnection conn)
    {
        Device device = ConnectedDevices.First(x => x.player.Connection == conn);
        if(device != null)
        {
            ConnectedDevices.Remove(device);
            Destroy(device.gameObject);
        }
    }

    private void AddPlayer(NetworkConnection conn, Player player)
    {
        Device createdDevice = Instantiate(_deviceTemplate, Vector3.zero, Quaternion.identity, transform);
        //createdDevice.Connection = conn;
        createdDevice.player = player;
        player.Connection = conn;
        ConnectedDevices.Add(createdDevice);
    }

    /*public Player GetPlayerWithNetworkIdentity(NetworkIdentity identity)
    {
        return ConnectedDevices.First(x => x.player.netIdentity == identity).player;
    }*/
}
