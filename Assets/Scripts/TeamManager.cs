using System.Collections.Generic;
using UnityEngine;

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
            CreateTeams();
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void CreateTeams()
    {
        teams = new List<Team>();
        Team team1 = Instantiate(_teamTemplate, Vector3.zero, Quaternion.identity, transform);
        team1.teamColor = Color.blue;
        team1.teamName = "Team 1";
        teams.Add(team1);

        Team team2 = Instantiate(_teamTemplate, Vector3.zero, Quaternion.identity, transform);
        team2.teamColor = Color.red;
        team2.teamName = "Team 2";
        teams.Add(team2);

        Team team3 = Instantiate(_teamTemplate, Vector3.zero, Quaternion.identity, transform);
        team3.teamColor = Color.green;
        team3.teamName = "Team 3";
        teams.Add(team3);

        Team team4 = Instantiate(_teamTemplate, Vector3.zero, Quaternion.identity, transform);
        team4.teamColor = Color.yellow;
        team4.teamName = "Team 4";
        teams.Add(team4);
    }
}
