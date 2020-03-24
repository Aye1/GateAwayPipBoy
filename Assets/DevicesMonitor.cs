using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class DevicesMonitor : GameMasterBehaviour
{
    [SerializeField] private Device _deviceTemplate;

    private List<Device> _connectedDevices;

    protected override void Init()
    {
        _connectedDevices = new List<Device>();
        CustomNetworkManager.Instance.OnClientConnectedToServer += AddClient;
        CustomNetworkManager.Instance.OnClientDisconnectedFromServer += RemoveClient;
    }


    private void AddClient(NetworkConnection conn)
    {
        Device createdDevice = Instantiate(_deviceTemplate, Vector3.zero, Quaternion.identity, transform);
        createdDevice.Connection = conn;
        _connectedDevices.Add(createdDevice);
    }

    private void RemoveClient(NetworkConnection conn)
    {
        Device device = _connectedDevices.First(x => x.Connection == conn);
        if(device != null)
        {
            _connectedDevices.Remove(device);
            Destroy(device.gameObject);
        }
    }
}
