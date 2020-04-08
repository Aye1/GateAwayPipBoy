
public static class TypesHelpers
{
    public static bool HasMatchingType(DisplayType displayType, PlayerType playerType)
    {
        bool res = false;
        switch (displayType)
        {
            case DisplayType.PhoneOnly:
                res = playerType == PlayerType.Phone;
                break;
            case DisplayType.TabletOnly:
                res = playerType == PlayerType.Tablet;
                break;
            case DisplayType.TabletAndPhone:
                res = playerType == PlayerType.Phone || playerType == PlayerType.Tablet;
                break;
            case DisplayType.Unknown:
                res = false;
                break;
        }
        return res;
    }
}
