using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum GameStatus { NotStarted, Started, Finished };

public class GameData : NetworkBehaviour
{
    public GameStatus status;
    public string gameName;
    
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("Starting game on client");
        if(netIdentity.isClient)
        {
            GamesProvider.Instance.CreateGame(this);
        }
    }
}
