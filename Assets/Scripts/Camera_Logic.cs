using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Camera_Logic : NetworkBehaviour
{
    [SerializeField] private Transform localPlayer;
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        Debug.Log("is owner: " + IsOwner);
        if (!IsOwner) return;
        else
        {
            Debug.Log("Help");
        }
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").transform.GetChild(0);
        Debug.Log(localPlayer);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(!IsOwner) return;
        if (localPlayer == null)
        {
            Debug.Log("unity");
            localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").transform.GetChild(0);
        }
        gameObject.transform.position = localPlayer.position;

    }
}
