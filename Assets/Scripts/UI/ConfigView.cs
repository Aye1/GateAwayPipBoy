using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;

public class ConfigView : MonoBehaviour
{

#pragma warning disable 0649
    [SerializeField] private TMP_Dropdown _deviceTypeDropdown;
    [SerializeField] private Button _saveButton;
#pragma warning restore 0649

    private Config _config;
    private bool _isDirty = true;

    // Start is called before the first frame update
    void Start()
    {
        PopulateDeviceTypeDropdown();
        _deviceTypeDropdown.onValueChanged.AddListener(OnDeviceTypeChanged);
    }

    private void Update()
    {
        _saveButton.interactable = CanSaveConfig();
    }

    public void SetConfig(Config config)
    {
        _deviceTypeDropdown.value = (int)config.deviceType;
        _config = new Config(config);
    }

    private void PopulateDeviceTypeDropdown()
    {
        // TODO: move list of available games in scriptable objects
        List<OptionData> options = new List<OptionData>();
        foreach (DeviceType type in Enum.GetValues(typeof(DeviceType)))
        {
            OptionData option = new OptionData();
            option.text = type.ToString();
            options.Add(option);
        }
        _deviceTypeDropdown.AddOptions(options);
    }

    private void OnDeviceTypeChanged(int value)
    {
        _config.deviceType = (DeviceType)value;
        _isDirty = true;
    }

    public void SaveConfig()
    {
        ConfigManager.Instance.SetConfig(_config);
        ConfigManager.Instance.SaveConfig();
    }

    public bool CanSaveConfig()
    {
        return _isDirty && ConfigManager.Instance.IsConfigValid(_config);
    }

    public void CloseView()
    {
        Destroy(gameObject);
    }
}
