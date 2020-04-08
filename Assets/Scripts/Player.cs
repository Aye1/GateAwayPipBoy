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

    public override void OnStartClient()
    {
        if(netIdentity.hasAuthority)
        {
            CmdSetPlayerType(CustomNetworkManager.Instance.playerType);
            PlayerInfoManager.Instance.CreatePlayerInfo(this);
        }
    }
}
