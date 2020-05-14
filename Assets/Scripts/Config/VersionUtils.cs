
using System;

public static class VersionUtils
{
    public static int GetMajorVersion(string versionString)
    {
        string[] tmp = SplitVersions(versionString);
        return Int16.Parse(tmp[0]);
    }

    public static int GetMinorVersion(string versionString)
    {
        return Int16.Parse(SplitVersions(versionString)[1]);
    }

    public static int GetRevisionVersion(string versionString)
    {
        return Int16.Parse(SplitVersions(versionString)[2]);
    }

    private static string[] SplitVersions(string versionString)
    {
        return versionString.Split(new char[]{ '.' });
    }

    public static bool Equals(string leftVersionString, string rightVersionString)
    {
        return leftVersionString == rightVersionString;
    }

    public static bool LeftGreaterThanRight(string leftVersionString, string rightVersionString)
    {
        if(GetMajorVersion(leftVersionString) > GetMajorVersion(rightVersionString))
        {
            return true;
        } else if (GetMinorVersion(leftVersionString) > GetMinorVersion(rightVersionString))
        {
            return true;
        } else if (GetRevisionVersion(leftVersionString) > GetRevisionVersion(rightVersionString))
        {
            return true;
        }
        return false;
    }

    public static bool LeftGreaterOrEqualThanRight(string leftVersionString, string rightVersionString)
    {
        return Equals(leftVersionString, rightVersionString) || LeftGreaterThanRight(leftVersionString, rightVersionString);
    }

    public static bool AreVersionsCompatible(string versionString1, string versionString2)
    {
        return GetMajorVersion(versionString1) == GetMajorVersion(versionString2)
            && GetMinorVersion(versionString1) == GetMinorVersion(versionString2);
    }
}
