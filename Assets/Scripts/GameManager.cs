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

    public void Awake()
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
        LaunchGameWithAllPlayers(GameType.TestGame);
    }

    public void LaunchSymbolGame()
    {
        LaunchGameWithAllPlayers(GameType.SymbolGame);
    }

    public void LaunchMazeGame()
    {
        LaunchGameWithAllPlayers(GameType.MazeGame);
    }

    public void LaunchGameWithAllPlayers(GameType type)
    {
        LaunchGame(type, CustomNetworkManager.Instance.ConnectedPlayers);
    }

    public void LaunchGame(GameType type, IEnumerable<Player> players)
    {
        GameData data = CreateGameData(type);
        foreach(Player player in players)
        {
            DevicesMonitor.Instance.SetCurrentGame(player, data);
            data.AddPlayer(player.netIdentity);
        }
        // TODO: check on view spawning if the player is in this game
        NetworkServer.Spawn(data.gameObject);
    }

    public GameData CreateGameData(GameType type)
    {
        GameBinding binding = GetGame(type);
        GameData data = Instantiate(binding.gameData, Vector3.zero, Quaternion.identity, transform);
        return data;
    }

    public GameControlData CreateControlData(GameType type)
    {
        GameBinding binding = GetGame(type);
        GameControlData data = Instantiate(binding.gameControlData, Vector3.zero, Quaternion.identity, transform);
        return data;
    }

    public void SendControlBroadcast(GameControlData control, IEnumerable<Player> players)
    {
        SendControlsBroadcast(new List<GameControlData>() { control }, players);
    }
    
    public void SendControlsBroadcast(IEnumerable<GameControlData> controls, IEnumerable<Player> players)
    {
        foreach(GameControlData control in controls)
        {
          foreach(Player player in players)
            {
                NetworkServer.Spawn(control.gameObject, player.gameObject);
            }
        }
    }

    public void SendControlsRoundRobin(IEnumerable<GameControlData> controls, IEnumerable<Player> players)
    {
        if (players != null && players.Count() > 0)
        {
            int nextPlayerIndex = 0;
            foreach (GameControlData controlData in controls)
            {
                SendControlData(controlData, players.ToArray()[nextPlayerIndex]);
                nextPlayerIndex = (nextPlayerIndex + 1) % players.Count();
            }
        }
    }

    public void SendControlData(GameControlData data, Player player)
    {
        NetworkServer.Spawn(data.gameObject, player.gameObject);
    }
}
