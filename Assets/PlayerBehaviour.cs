using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBehaviour : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(netIdentity.isClient);
        Init();
    }

    protected virtual void Init() { }
}
