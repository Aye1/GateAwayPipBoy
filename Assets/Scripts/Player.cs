using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

[Serializable]
public class Player : NetworkBehaviour
{
    public NetworkConnection Connection { get; set; }

    [SyncVar]
    public string debugInfo = "test";

    [Command]
    public void CmdSetDebugInfo(string info)
    {
        debugInfo = info;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(netIdentity.hasAuthority)
        {
            PlayerInfoManager.Instance.CreatePlayerInfo(this);
        }
    }
}
