
public static class TypesHelpers
{
    public static bool HasMatchingType(DisplayType displayType, DeviceType deviceType)
    {
        bool res = false;
        switch (displayType)
        {
            case DisplayType.PhoneOnly:
                res = deviceType == DeviceType.Phone;
                break;
            case DisplayType.TabletOnly:
                res = deviceType == DeviceType.Tablet;
                break;
            case DisplayType.TabletAndPhone:
                res = deviceType == DeviceType.Phone || deviceType == DeviceType.Tablet;
                break;
            case DisplayType.Unknown:
                res = false;
                break;
        }
        return res;
    }
}
