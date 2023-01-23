using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Camera_Logic : NetworkBehaviour
{
    //serialize only for debugging
    [SerializeField] private Transform localPlayer;
    [SerializeField] private Transform localPlayerOrientation;
    private float sensitivityY = 1000;
    private float rotationY;
    bool cursorLocked = false;
    // Update is called once per frame
    public override void OnNetworkSpawn()
    {
        //cursorLocked = true;
        changeCursorState();
    }

    void Update()
    {
        if (localPlayer == null)
            return;
        if (Input.GetKeyUp(KeyCode.P))
        {
            changeCursorState();
        }
        if (!Cursor.visible)
        {
            rotationY += Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityY; //rotation around Y axis
            localPlayer.rotation = Quaternion.Euler(0, rotationY, 0);
        }

        transform.rotation = localPlayer.rotation;
    }

    public void changeCursorState()
    {
        if (cursorLocked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = cursorLocked;
        cursorLocked = !cursorLocked;
    }
    void LateUpdate()
    {
        //Move with player
        if (localPlayer != null)
            gameObject.transform.position = localPlayerOrientation.position;
    }

    //Initialize transform at eyeheight of the player model
    public void InitLocalPlayer(Transform player, Character character = Character.Null)
    {
        localPlayer = player;
        localPlayerOrientation = localPlayer.GetChild(1).GetChild(0).GetChild(1);
        if (character == Character.Donus)
        { 
            GetComponent<Camera>().farClipPlane = 0.03f; 
        }
    }
}
