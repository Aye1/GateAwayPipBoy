using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TMPro.TMP_Dropdown;

public class TeamSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    private Team[] _teams;

    // Start is called before the first frame update
    void Start()
    {
        _dropdown = GetComponentInChildren<TMP_Dropdown>();
        PopulateDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateDropdown()
    {
        _teams = TeamManager.Instance.teams.ToArray();
        List<OptionData> options = new List<OptionData>();
        for(int i = 0; i < _teams.Length; i++)
        {
            OptionData option = new OptionData();
            option.text = _teams[i].teamName;
            options.Add(option);
        }
        _dropdown.AddOptions(options);
    }
}
