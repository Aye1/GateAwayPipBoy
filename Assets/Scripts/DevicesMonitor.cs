using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class DevicesMonitor : GameMasterBehaviour
{
#pragma warning disable 0649
    [SerializeField] private DeviceView _deviceTemplate;
#pragma warning restore 0649

    public static DevicesMonitor Instance { get; private set; }

    public List<DeviceView> ConnectedDevices { get; private set; }

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
        ConnectedDevices = new List<DeviceView>();
        CustomNetworkManager.OnClientDisconnectedFromServer += RemoveClient;
        CustomNetworkManager.OnPlayerAddedToServer += AddPlayer;
    }

    private void RemoveClient(NetworkConnection conn)
    {
        DeviceView device = ConnectedDevices.First(x => x.player.Connection == conn);
        if(device != null)
        {
            ConnectedDevices.Remove(device);
            Destroy(device.gameObject);
        }
    }

    private void AddPlayer(Player player)
    {
        DeviceView createdDevice = Instantiate(_deviceTemplate, Vector3.zero, Quaternion.identity, transform);
        createdDevice.player = player;
        ConnectedDevices.Add(createdDevice);
    }

    public void SetCurrentGame(Player player, GameData gameData)
    {
        DeviceView currentDevice = ConnectedDevices.First(x => x.player == player);
        if(currentDevice != null)
        {
            currentDevice.SetCurrentGame(gameData);
        }
    }

}
