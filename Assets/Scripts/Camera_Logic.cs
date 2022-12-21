using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Camera_Logic : NetworkBehaviour
{
    //serialize only for debugging
    [SerializeField] private Transform localPlayer;
    [SerializeField] private Transform localPlayerOrientation;
    private float sensitivtyX = 1000, sensitivityY = 1000;
    private float rotationX, rotationY;
    bool cursorLocked = false;
    // Update is called once per frame
    void Update()
    {
        if (localPlayer == null)
            return;
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (cursorLocked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = cursorLocked;
            cursorLocked = !cursorLocked;
            Debug.Log("up");
        }
        if (!Cursor.visible)
        {
            rotationY += Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityY; //rotation around Y axis
            //rotationX -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivtyX; //rotation around X axis (potentially remove this DOF, since we need to keep aiming simple
            //rotationX = Mathf.Clamp(rotationX, -90, 90);
            //rotationX = m
            //transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
            localPlayer.rotation = Quaternion.Euler(0, rotationY, 0); //the entire model also looks up, just a placeholder
        }

        transform.rotation = localPlayer.rotation;
    }
    void LateUpdate()
    {
        //Move with player
        if (localPlayer != null)
            gameObject.transform.position = localPlayerOrientation.position;
    }

    //Initialize transform at eyeheight of the player model
    public void InitLocalPlayer(Transform player)
    {
        localPlayer = player;
        localPlayerOrientation = localPlayer.GetChild(0);

    }
}
