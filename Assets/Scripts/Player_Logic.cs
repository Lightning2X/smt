using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player_Logic : NetworkBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Transform orientation;
    private Vector3 localPlayerVelocity;
    private float movementSpeed = 3;
    void Update()
    {
        //return if it's not the local player
        if (!IsOwner) return;
        PlayerMovement();
    }

    //rigidbody doesn't seem to work with FixedUpdate
    //void FixedUpdate()
    //{
    //    if (!IsOwner) return;        
    //}
    private void PlayerMovement()
    {
        //reset velocity dir
        localPlayerVelocity = Vector3.zero;
        //We could make configurable keys.
        if (Input.GetKey(KeyCode.W))
        {
            localPlayerVelocity += orientation.forward;
            Debug.Log(localPlayerVelocity);
        }
        if (Input.GetKey(KeyCode.A)) localPlayerVelocity -= orientation.right;
        if (Input.GetKey(KeyCode.S)) localPlayerVelocity -= orientation.forward;
        if (Input.GetKey(KeyCode.D)) localPlayerVelocity += orientation.right;
        playerRB.AddForce(localPlayerVelocity.normalized * movementSpeed, ForceMode.Force);
    }
}
