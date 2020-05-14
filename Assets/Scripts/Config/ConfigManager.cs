using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeviceType { Unknown, Server, Tablet, Phone };
public class ConfigManager : MonoBehaviour
{
    // Version string is in format M.m.r
    // M: major version, changes if the whole system changes
    // m: minor version, changes every time the version compatibility is broken (e.g. field removed)
    // r: revision version, changes with every config change, if it does not break the compatibility (e.g. field added)
    public const string currentConfigVersion = "0.0.1";

    private const string configVersionKey = "CONFIG_VERSION";
    private const string deviceTypeKey = "DEVICE_TYPE";

    public static ConfigManager Instance { get; private set; }

    public bool isConfLoaded;

    [Header("UI bindings")]
    [SerializeField] private ConfigView _configView;
    [SerializeField] private Canvas _canvas;

    [Header("Configuration values")]
    public DeviceType deviceType = DeviceType.Unknown;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(LocalConfigExists())
        {
            Debug.Log("Local config found");
            LoadLocalConfig();
        } else
        {
            LaunchConfigSetup();
        }
    }

    public bool LocalConfigExists()
    {
        return PlayerPrefs.GetString(configVersionKey) != "";
    }

    private void LoadLocalConfig()
    {
        string configToLoadVersion = PlayerPrefs.GetString(configVersionKey);
        if (!VersionUtils.AreVersionsCompatible(currentConfigVersion, configToLoadVersion))
        {
            Debug.LogWarning("Config found incompatible with current version");
            LaunchConfigSetup();
        } else
        {
            deviceType = (DeviceType)PlayerPrefs.GetInt(deviceTypeKey);
            if(!VersionUtils.Equals(currentConfigVersion, configToLoadVersion))
            {
                // Save config with new version, some fields may be saved with default values
                SaveConfig();
                Debug.LogWarning("Resaving config with latest version, check that everything is OK");
            }
            isConfLoaded = true;
        }
    }

    public void SaveConfig()
    {
        PlayerPrefs.SetInt(deviceTypeKey, (int) deviceType);
        PlayerPrefs.SetString(configVersionKey, currentConfigVersion);
    }

    private void LaunchConfigSetup()
    {
        ConfigView view = Instantiate(_configView, Vector3.zero, Quaternion.identity, _canvas.transform);
        view.SetDeviceType(deviceType);
        view.transform.localPosition = Vector3.zero;
    }
}
