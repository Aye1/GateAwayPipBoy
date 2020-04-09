using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static TMPro.TMP_Dropdown;

public class TeamSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    private List<Team> _teams;

    #region Events
    public delegate void TeamChanged(Team newTeam);
    public event TeamChanged OnTeamChanged;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _dropdown = GetComponentInChildren<TMP_Dropdown>();
        _dropdown.onValueChanged.AddListener(delegate { OnTeamValueChanges(); });
        if (TeamManager.Instance.teamsCreated)
        {
            PopulateDropdown();
        } else
        {
            TeamManager.OnTeamsCreated += PopulateDropdown;
        }
    }

    private void PopulateDropdown()
    {
        _teams = TeamManager.Instance.teams;
        List<OptionData> options = new List<OptionData>();
        foreach(Team team in _teams) { 
            OptionData option = new OptionData();
            option.text = team.teamName;
            options.Add(option);
        }
        _dropdown.AddOptions(options);
    }

    private void OnTeamValueChanges()
    {
        Team newTeam = TeamForOption(_dropdown.value);
        OnTeamChanged?.Invoke(newTeam);
    }

    public void SetTeam(Team team)
    {
        SetTeam(team.teamId);
    }

    public void SetTeam(int teamId)
    {
        _dropdown.value = teamId;
    }

    public Team GetTeam()
    {
        return TeamForOption(_dropdown.value);
    }

    private Team TeamForOption(int option)
    {
        return _teams.ToArray()[option];
    }

    private int OptionForTeam(Team team)
    {
        return _teams.IndexOf(team);
    }
}
