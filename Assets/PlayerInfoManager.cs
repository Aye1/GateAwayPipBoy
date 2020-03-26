using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{

    [SerializeField] private PlayerInfoPanel _infoPanel;

    public static PlayerInfoManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreatePlayerInfo(Player player)
    {
        PlayerInfoPanel createdPanel = Instantiate(_infoPanel, Vector3.zero, Quaternion.identity, transform);
        createdPanel.player = player;
    }
}
