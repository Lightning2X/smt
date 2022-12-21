using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player_Logic : NetworkBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Transform playerTransform;
    private Vector3 localPlayerVelocity;
    private float movementSpeed = 3;
    private string playerName = "LocalPlayer";
    private Camera_Logic cam;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        gameObject.name = playerName;
        //gameObject.tag = playerName;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Logic>();
        cam.InitLocalPlayer(gameObject.transform);
    }
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
    //    if(localPlayerVelocity != Vector3.zero)
    //    {
    //        Debug.Log(localPlayerVelocity);
    //    }
    //    playerRB.AddForce(localPlayerVelocity.normalized * movementSpeed, ForceMode.Force);
    //}
    private void PlayerMovement()
    {
        //reset velocity dir
        localPlayerVelocity = Vector3.zero;
        //We could make configurable keys. use forward etc for when you move around the camera
        if (Input.GetKey(KeyCode.W))
        {
            localPlayerVelocity += playerTransform.forward;
            //Debug.Log("Is host: " + IsHost);
        }
        if (Input.GetKey(KeyCode.A)) localPlayerVelocity -= playerTransform.right;
        if (Input.GetKey(KeyCode.S)) localPlayerVelocity -= playerTransform.forward;
        if (Input.GetKey(KeyCode.D)) localPlayerVelocity += playerTransform.right;

        //Rigidbody somehow doesn't work if you build the game, but it does work with the editor. (For host and client the same)
        //playerRB.AddForce(localPlayerVelocity.normalized * movementSpeed, ForceMode.Force);

        //This somehow does work for editor and build, but the smoothness depends on the refreshrate of the monitor that the game starts on.
        //deltaTime and fixedDeltaTime doesn't do anything.
        localPlayerVelocity.y = 0;
        playerTransform.position += localPlayerVelocity.normalized * movementSpeed * Time.deltaTime;
    }
}
