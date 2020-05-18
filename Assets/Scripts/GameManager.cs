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

   [HideInInspector] public static readonly string allSymbols = "$%#@!*1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

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

    public bool CanLaunchGame()
    {
        return NetworkServer.active;
    }

    public bool CanLaunchGame(Team team)
    {
        return CanLaunchGame() && TeamManager.Instance.GetPlayersOfTeam(team).Count() > 0;
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

    public void SendControlBroadcast(GameControlData control, IEnumerable<NetworkIdentity> playerIdentities)
    {
        SendControlsBroadcast(new List<GameControlData>() { control }, playerIdentities);
    }
    
    public void SendControlsBroadcast(IEnumerable<GameControlData> controls, IEnumerable<NetworkIdentity> playerIdentities)
    {
        foreach(GameControlData control in controls)
        {
          foreach(NetworkIdentity player in playerIdentities)
            {
                NetworkServer.Spawn(control.gameObject, player.gameObject);
            }
        }
    }

    /// <summary>
    /// Sends the controls to all given players, in a round-robin repartition
    /// </summary>
    /// <param name="controls">The list of controls to send</param>
    /// <param name="players">The list of players playing this game</param>
    /// <returns>A dictionary matching each control with its player</returns>
    public Dictionary<GameControlData, Player> SendControlsRoundRobin(IEnumerable<GameControlData> controls, IEnumerable<Player> players)
    {
        Dictionary<GameControlData, Player> repartition = new Dictionary<GameControlData, Player>();
        if (players != null && players.Count() > 0)
        {
            int nextPlayerIndex = 0;
            foreach (GameControlData controlData in controls)
            {
                Player currentPlayer = players.ToArray()[nextPlayerIndex];
                SendControlData(controlData, currentPlayer);
                nextPlayerIndex = (nextPlayerIndex + 1) % players.Count();
                repartition.Add(controlData, currentPlayer);
            }
        }
        return repartition;
    }

    public Dictionary<GameControlData, Player> SendControlsRoundRobin(IEnumerable<GameControlData> controls, IEnumerable<NetworkIdentity> playerIdentities)
    {
        IEnumerable<Player> players = playerIdentities.Select(x => x.GetComponent<Player>());
        return SendControlsRoundRobin(controls, players);
    }

    public void SendControlData(GameControlData data, Player player)
    {
        NetworkServer.Spawn(data.gameObject, player.gameObject);
    }

    public void ClearAllGameData()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }
}
