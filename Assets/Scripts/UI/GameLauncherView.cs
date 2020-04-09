using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TMPro.TMP_Dropdown;

public class GameLauncherView : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TMP_Dropdown _gameDropdown;
    [SerializeField] private Button _launchButton;
    [SerializeField] private TeamSelector _teamSelector;
#pragma warning restore 0649

    // Start is called before the first frame update
    void Start()
    {
        PopulateGameDropdown();
        _launchButton.onClick.AddListener(LaunchGame);
    }

    // Update is called once per frame
    void Update()
    {
        _launchButton.interactable = GameManager.Instance.CanLaunchGame(_teamSelector.GetTeam());
    }

    private void LaunchGame()
    {
        GameManager.Instance.LaunchGame(GetSelectedGameType(), TeamManager.Instance.GetPlayersOfTeam(_teamSelector.GetTeam()));
    }

    private void PopulateGameDropdown()
    {
        // TODO: move list of available games in scriptable objects
        List<OptionData> options = new List<OptionData>();
        foreach (GameType type in Enum.GetValues(typeof(GameType)))
        {
            OptionData option = new OptionData();
            option.text = type.ToString();
            options.Add(option);
        }
        _gameDropdown.AddOptions(options);
    }

    private GameType GetSelectedGameType()
    {
        return (GameType)_gameDropdown.value;
    }
}
