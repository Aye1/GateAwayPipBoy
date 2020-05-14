using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static TMPro.TMP_Dropdown;

public class ConfigView : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _deviceTypeDropdown;

    // Start is called before the first frame update
    void Start()
    {
        PopulateDeviceTypeDropdown();
    }

    public void SetDeviceType(DeviceType type)
    {
        _deviceTypeDropdown.value = (int)type;
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

    public void SaveConfig()
    {
        ConfigManager.Instance.deviceType = (DeviceType)_deviceTypeDropdown.value;
        ConfigManager.Instance.SaveConfig();
    }

    public void CloseView()
    {
        Destroy(gameObject);
    }
}
