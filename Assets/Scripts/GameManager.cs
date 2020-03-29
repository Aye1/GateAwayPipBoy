using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

[Serializable]
public struct GameBinding
{
    public GameType gameType;
    public GameData gameData;
    public GameView gameView;
    public GameControlData gameControlData;
    public GameControlView gameControlView;
}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public List<GameBinding> games;

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

    public GameBinding GetGame(GameType type)
    {
        return games.FirstOrDefault(x => x.gameType == type);
    }

    public void LaunchTestGame()
    {
        LaunchGame(GameType.TestGame);
    }

    public void LaunchGame(GameType type)
    {
        GameBinding binding = GetGame(type);
        GameData data = Instantiate(binding.gameData);
        NetworkServer.Spawn(data.gameObject);
        CustomNetworkManager.Instance.ConnectedPlayers.ForEach(x => DevicesMonitor.Instance.SetCurrentGame(x, data));
        CreateControls(binding, data);
    }

    // Basic control repartition
    // Each player has the same control
    // TODO: refacto and adapt to each game
    public void CreateControls(GameBinding binding, GameData mainData)
    {
        foreach (Player player in CustomNetworkManager.Instance.ConnectedPlayers)
        {
            GameControlData controlData = Instantiate(binding.gameControlData);
            NetworkServer.Spawn(controlData.gameObject, player.gameObject);
        }
    }

    // TODO: refacto this part
    #region Symbol Game
    public void LaunchSymbolGame()
    {
        GameBinding binding = GetGame(GameType.SymbolGame);
        SymbolGameData gameData = (SymbolGameData)Instantiate(binding.gameData);
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
        GameBinding binding = GetGame(GameType.SymbolGame);
        SymbolControlData createdData = (SymbolControlData)Instantiate(binding.gameControlData);
        createdData.symbol = symbol;
        return createdData;
    }

    public void SendSymbolControl(SymbolControlData data, Player player)
    {
        NetworkServer.Spawn(data.gameObject, player.gameObject);
    }
    #endregion
}
