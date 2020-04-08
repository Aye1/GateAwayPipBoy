using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Team : NetworkBehaviour
{
    public List<Player> teamPlayers;

    //[SyncVar]
    public string teamName;

    public Color teamColor;

    /*[Command]
    public void CmdSetTeamName(string name)
    {
        teamName = name;
    }*/
}
