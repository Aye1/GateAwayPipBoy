using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class GameMasterBehaviour : MonoBehaviour
{
    protected NetworkIdentity _identity;

    // Start is called before the first frame update
    void Start()
    {
        _identity = GetComponent<NetworkIdentity>();
        gameObject.SetActive(_identity.isServer);
        Init();
    }

    protected virtual void Init() { }
}
