using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private PlayerInfoPanel _infoPanel;
#pragma warning restore 0649

    public static PlayerInfoManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreatePlayerInfo(Player player)
    {
        PlayerInfoPanel createdPanel = Instantiate(_infoPanel, Vector3.zero, Quaternion.identity, transform);
        createdPanel.transform.localPosition = Vector3.zero;
        createdPanel.player = player;
    }
}
