using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeviceView : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI _connectionIdText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _addressText;
    [SerializeField] private TextMeshProUGUI _debugInfo;
    [SerializeField] private GameInfoPanel _gameInfo;
    [SerializeField] private TeamSelector _teamSelector;
#pragma warning restore 0649

    public Player player;

    private void Start()
    {
        _teamSelector.OnTeamChanged += UpdateTeam;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.Connection != null)
        {
            _connectionIdText.text = "Connection " + player.Connection.connectionId;
            _typeText.text = player.playerType.ToString();
            _addressText.text = player.Connection.address;
            _debugInfo.text = player.playerName;
            if (player.teamIdentity != null)
            {
                Team startTeam = player.teamIdentity.GetComponent<Team>();
                _teamSelector.SetTeam(startTeam);
            }
        }
        else
        {
            _connectionIdText.text = "Player not found";
            _typeText.text = "N/A";
            _addressText.text = "N/A";
            _debugInfo.text = "N/A";
        }
    }

    public void SetCurrentGame(GameData gameData)
    {
        _gameInfo.gameData = gameData;
    }

    public GameData GetCurrentGame()
    {
        return _gameInfo.gameData;
    }

    private void UpdateTeam(Team team)
    {
        //int teamId = TeamManager.Instance.GetTeamId(team);
        //player.CmdSetTeamIdentity(team.netIdentity);
        // We are on the server, we don't need to call the command (+ it does not work)
        player.teamIdentity = team.netIdentity;
        //player.CmdSetTeamId(teamId);
    }
}
