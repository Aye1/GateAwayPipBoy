using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class MovePlayer : MonoBehaviour
{

    public float speed = 0.05f;
    private NetworkIdentity _identity;

    // Start is called before the first frame update
    void Start()
    {
        _identity = GetComponent<NetworkIdentity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_identity.isLocalPlayer) {
            Vector3 destPos = transform.position;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                destPos += Vector3.up * speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                destPos += Vector3.down * speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                destPos += Vector3.left * speed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                destPos += Vector3.right * speed;
            }

            transform.position = Vector3.MoveTowards(transform.position, destPos, 1);
        }
    }
}
