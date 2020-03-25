using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public NetworkConnection Connection { get; set; }

    [SyncVar]
    public string debugInfo = "test";

    private int debugInt;

    private void Update()
    {
        CmdUpdateInfo();
    }

    [Command]
    public void CmdUpdateInfo()
    {
        debugInt++;
        debugInfo = debugInt.ToString();
    }
}
