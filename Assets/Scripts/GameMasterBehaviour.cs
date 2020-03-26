using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameMasterBehaviour : NetworkBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(netIdentity.isServer);
        Init();
    }

    protected virtual void Init() { }
}
