using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{

    [SerializeField] private PlayerInfoPanel _infoPanel;

    // Start is called before the first frame update
    void Start()
    {
        CustomNetworkManager.OnPlayerAddedToServer += OnPlayerAddedToServer;
    }

    private void OnPlayerAddedToServer(Player player)
    {
        PlayerInfoPanel createdPanel = Instantiate(_infoPanel, Vector3.zero, Quaternion.identity, transform);
        createdPanel.player = player;
    }
}
