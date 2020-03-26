using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

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
}
