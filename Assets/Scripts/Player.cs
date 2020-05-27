using Mirror;
using System;

public enum DeviceType { Unknown, Server, Tablet, Phone };

[Serializable]
public class Player : NetworkBehaviour
{
    public NetworkConnection Connection { get; set; }

    [SyncVar]
    public string playerName = "N/A";

    [SyncVar]
    public DeviceType deviceType;

    [SyncVar]
    public NetworkIdentity teamIdentity;

    [Command]
    public void CmdSetPlayerName(string name)
    {
        playerName = name;
    }

    [Command]
    public void CmdSetDeviceType(DeviceType type)
    {
        deviceType = type;
    }

    [Command]
    public void CmdSetTeamIdentity(NetworkIdentity identity)
    {
        teamIdentity = identity;
    }

    public override void OnStartClient()
    {
        if(netIdentity.hasAuthority)
        {
            CmdSetDeviceType(CustomNetworkManager.Instance.deviceType);
            // We init every player on first team 
            NetworkIdentity defaultTeamIdentity = TeamManager.Instance.GetTeam(0).netIdentity;
            CmdSetTeamIdentity(defaultTeamIdentity);
            PlayerInfoManager.Instance.CreatePlayerInfo(this);
        }
    }

    [Command]
    public void CmdAskForSymbolPartialResult(NetworkIdentity identity)
    {
        SymbolGameData gameData = (SymbolGameData) DevicesMonitor.Instance.GetCurrentGame(identity.GetComponent<Player>());
        gameData.SendPartialResult(identity);
    }

    public void AskForSymbolPartialResult()
    {
        CmdAskForSymbolPartialResult(netIdentity);
    }
}
