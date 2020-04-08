using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Team : NetworkBehaviour
{
    public List<Player> teamPlayers;

    [SyncVar]
    public string teamName;

    public Color teamColor;

    [SyncVar]
    public int teamId;

    /*[Command]
    public void CmdSetTeamName(string name)
    {
        teamName = name;
    }*/

    // Used to rebuild the team list client side
    // TODO: see if it could be replaced by a SyncList
    public override void OnStartClient()
    {
        TeamManager.Instance.ClientAddTeam(this);
        transform.SetParent(TeamManager.Instance.transform);
    }
}
