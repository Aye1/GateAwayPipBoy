using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private List<GameData> _currentGames;
    public Transform gamesHolder;
    public GameData gameDataTemplate;

    public GameObject dummyDebug;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _currentGames = new List<GameData>();   
    }


    public void DebugSendGameToAllClients()
    {
        foreach(Player player in CustomNetworkManager.Instance.ConnectedPlayers)
        {
            GameData createdGame = Instantiate(gameDataTemplate);
            _currentGames.Add(createdGame);
            SendGame(createdGame, player);
        }
    }

    public void SendGame(GameData game, Player player)
    {
        NetworkServer.Spawn(game.gameObject, player.gameObject);
        game.transform.position = Vector3.zero;
    }
}
