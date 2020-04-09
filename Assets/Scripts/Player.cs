using Mirror;
using System;

public enum PlayerType { Unknown, Tablet, Phone };

[Serializable]
public class Player : NetworkBehaviour
{
    public NetworkConnection Connection { get; set; }

    [SyncVar]
    public string playerName = "N/A";

    [SyncVar]
    public PlayerType playerType;

    [SyncVar]
    public NetworkIdentity teamIdentity;

    [Command]
    public void CmdSetPlayerName(string name)
    {
        playerName = name;
    }

    [Command]
    public void CmdSetPlayerType(PlayerType type)
    {
        playerType = type;
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
            CmdSetPlayerType(CustomNetworkManager.Instance.playerType);
            // We init every player on first team 
            NetworkIdentity defaultTeamIdentity = TeamManager.Instance.GetTeam(0).netIdentity;
            CmdSetTeamIdentity(defaultTeamIdentity);
            PlayerInfoManager.Instance.CreatePlayerInfo(this);
        }
    }
}
