using Mirror;
using System;

[Serializable]
public class Player : NetworkBehaviour
{
    public NetworkConnection Connection { get; set; }

    [SyncVar]
    public string debugInfo = "N/A";

    [Command]
    public void CmdSetDebugInfo(string info)
    {
        debugInfo = info;
    }

    public override void OnStartClient()
    {
        if(netIdentity.hasAuthority)
        {
            PlayerInfoManager.Instance.CreatePlayerInfo(this);
        }
    }
}
