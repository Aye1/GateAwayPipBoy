
public class Config
{
    // Version string is in format M.m.r
    // M: major version, changes if the whole system changes
    // m: minor version, changes every time the version compatibility is broken (e.g. field removed)
    // r: revision version, changes with every config change, if it does not break the compatibility (e.g. field added)
    public string version = "0.1.0";
    public DeviceType deviceType = DeviceType.Unknown;

    public Config() {}

    public Config(Config config)
    {
        this.version = config.version;
        this.deviceType = config.deviceType;
    }
}
