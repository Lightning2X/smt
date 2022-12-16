using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Camera_Logic : NetworkBehaviour
{
    //serialize only for debugging
    [SerializeField] private Transform localPlayer;

    // Update is called once per frame
    void LateUpdate()
    {
        //Move with player
        if (localPlayer != null)
            gameObject.transform.position = localPlayer.position;
    }

    //Initialize transform at eyeheight of the player model
    public void InitLocalPlayer(Transform player)
    {
        localPlayer = player.GetChild(0);
    }
}
