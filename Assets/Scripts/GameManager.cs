using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private List<GameData> _currentGames;
    public Transform gamesHolder;
    public GameData gameDataTemplate;
    public SymbolControlData symbolControlDataTemplate;
    public SymbolGameData symbolGameDataTemplate;

   [HideInInspector] public static readonly string allSymbols = "$%#@!*1234567890;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

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
            createdGame.CmdAddPlayer(player.netIdentity);
            SendGame(createdGame, player);
        }
    }

    public void SendGame(GameData game, Player player)
    {
        NetworkServer.Spawn(game.gameObject, player.gameObject);
        game.transform.position = Vector3.zero;
        DevicesMonitor.Instance.SetCurrentGame(player, game);
    }

    #region Symbol Game
    public void LaunchSymbolGame()
    {
        SymbolGameData gameData = Instantiate(symbolGameDataTemplate);
        NetworkServer.Spawn(gameData.gameObject);
        SpawnSymbolGame(gameData);
    }

    public void SpawnSymbolGame(SymbolGameData gameData)
    {
        List<Player> players = CustomNetworkManager.Instance.ConnectedPlayers;
        players.ForEach(x => DevicesMonitor.Instance.SetCurrentGame(x, gameData));
        string result = gameData.result;
        // Shuffling the characters order
        List<char> possibleSymbolsShuffled = result.ToCharArray().OrderBy(x => Guid.NewGuid()).ToList();
        int nextPlayerIndex = 0;

        while(possibleSymbolsShuffled.Count > 0)
        {
            char nextChar = possibleSymbolsShuffled[0];
            possibleSymbolsShuffled.RemoveAt(0);
            SymbolControlData data = CreateSymbolData(nextChar, gameData);
            SendSymbolControl(data, players[nextPlayerIndex]);
            nextPlayerIndex = (nextPlayerIndex + 1) % players.Count;
        }
    }

    private SymbolControlData CreateSymbolData(char symbol, SymbolGameData mainGameData)
    {
        SymbolControlData createdData = Instantiate(symbolControlDataTemplate);
        createdData.symbol = symbol;
        createdData.mainGameData = mainGameData;
        return createdData;
    }

    public void SendSymbolControl(SymbolControlData data, Player player)
    {
        NetworkServer.Spawn(data.gameObject, player.gameObject);
        data.transform.position = Vector3.zero;
    }
    #endregion
}
