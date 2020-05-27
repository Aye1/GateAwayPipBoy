using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    private const string configVersionKey = "CONFIG_VERSION";
    private const string deviceTypeKey = "DEVICE_TYPE";

    public static ConfigManager Instance { get; private set; }

    public bool isConfLoaded;
    public Config currentConfig;

    [Header("UI bindings")]
    [SerializeField] private ConfigView _configView;
    [SerializeField] private Canvas _canvas;

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
        currentConfig = new Config();
        if (LocalConfigExists())
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
        currentConfig.deviceType = (DeviceType)PlayerPrefs.GetInt(deviceTypeKey);

        if (!VersionUtils.AreVersionsCompatible(currentConfig.version, configToLoadVersion))
        {
            Debug.LogWarning("Config found incompatible with current version");
            LaunchConfigSetup();
        } else
        {
            if(!VersionUtils.Equals(currentConfig.version, configToLoadVersion))
            {
                // Save config with new version, some fields may be saved with default values
                SaveConfig();
                Debug.LogWarning("Resaving config with latest version, check that everything is OK");
                isConfLoaded = true;
            }
        }
    }

    public void SetConfig(Config config)
    {
        currentConfig = config;
    }

    public void SaveConfig()
    {
        PlayerPrefs.SetInt(deviceTypeKey, (int) currentConfig.deviceType);
        PlayerPrefs.SetString(configVersionKey, currentConfig.version);
    }

    private void LaunchConfigSetup()
    {
        ConfigView view = Instantiate(_configView, Vector3.zero, Quaternion.identity, _canvas.transform);
        view.SetConfig(currentConfig);
        view.transform.localPosition = Vector3.zero;
    }

    public bool IsConfigValid(Config config)
    {
        bool isValid = VersionUtils.HasCorrectFormat(config.version);
        isValid &= config.deviceType != DeviceType.Unknown;
        return isValid;
    }
}
