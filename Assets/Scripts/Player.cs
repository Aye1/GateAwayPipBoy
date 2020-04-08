using Mirror;
using System;

public enum PlayerType { Unknown, Tablet, Phone };

[Serializable]
public class Player : NetworkBehaviour
{
    public NetworkConnection Connection { get; set; }

    [SyncVar]
    public string debugInfo = "N/A";

    [SyncVar]
    public PlayerType playerType;

    /*[SyncVar]
    public int teamId;*/

    [SyncVar]
    public NetworkIdentity teamIdentity;

    [Command]
    public void CmdSetDebugInfo(string info)
    {
        debugInfo = info;
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

    /*[Command]
    public void CmdSetTeamId(int id)
    {
        teamId = id;
    }*/

    /*public override void OnStartServer()
    {
        //CmdSetTeamIdentity(TeamManager.Instance.GetTeam(2).netIdentity);
        NetworkIdentity defaultTeamIdentity = TeamManager.Instance.GetTeam(2).netIdentity;
        teamIdentity = defaultTeamIdentity;
    }*/

    public override void OnStartClient()
    {
        if(netIdentity.hasAuthority)
        {
            CmdSetPlayerType(CustomNetworkManager.Instance.playerType);
            // We init every player on first team 
            //CmdSetTeamId(2);
            NetworkIdentity defaultTeamIdentity = TeamManager.Instance.GetTeam(2).netIdentity;
            //teamIdentity = defaultTeamIdentity;
            CmdSetTeamIdentity(defaultTeamIdentity);
            PlayerInfoManager.Instance.CreatePlayerInfo(this);
        }
    }
}
