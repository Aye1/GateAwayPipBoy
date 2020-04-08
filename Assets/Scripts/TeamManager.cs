using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamManager : MonoBehaviour
{
    [SerializeField] private Team _teamTemplate;

    public static TeamManager Instance { get; private set; }
    public List<Team> teams;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            teams = new List<Team>();
            CustomNetworkManager.OnServerStarted += CreateTeams;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    public void CreateTeams()
    {
        Team team1 = Instantiate(_teamTemplate, Vector3.zero, Quaternion.identity, transform);
        team1.teamColor = Color.blue;
        team1.teamName = "Team 1";
        team1.teamId = 0;
        NetworkServer.Spawn(team1.gameObject);
        teams.Add(team1);

        Team team2 = Instantiate(_teamTemplate, Vector3.zero, Quaternion.identity, transform);
        team2.teamColor = Color.red;
        team2.teamName = "Team 2";
        team2.teamId = 1;
        NetworkServer.Spawn(team2.gameObject);
        teams.Add(team2);

        Team team3 = Instantiate(_teamTemplate, Vector3.zero, Quaternion.identity, transform);
        team3.teamColor = Color.green;
        team3.teamName = "Team 3";
        team3.teamId = 2;
        NetworkServer.Spawn(team3.gameObject);
        teams.Add(team3);

        Team team4 = Instantiate(_teamTemplate, Vector3.zero, Quaternion.identity, transform);
        team4.teamColor = Color.yellow;
        team4.teamName = "Team 4";
        team4.teamId = 3;
        NetworkServer.Spawn(team4.gameObject);
        teams.Add(team4);
    }

    public Team GetTeam(int id)
    {
        if(id >= 0 && id < teams.Count)
        {
            return teams.ToArray()[id];
        }
        return null;
    }

    public int GetTeamId(Team team)
    {
        return teams.IndexOf(team);
    }

    public void ClientAddTeam(Team team)
    {
        teams.Add(team);
    }
}
