using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;

public class LightOutSwitch : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject lightOutUI;

    public bool Action = false;
    public bool IsOpen = false;
    Animator anim;
    Camera_Logic camera_Logic;
    private bool switched = false;

    public override void OnNetworkSpawn()
    {
        camera_Logic=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Logic>();
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Action = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (IsOpen == true)
        {
            IsOpen = false;
        }
        if (switched) switched = false;
        //else camera_Logic.changeCursorState();
        lightOutUI.SetActive(false);
        Action = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action)
            {
                lightOutUI.SetActive(true);
                camera_Logic.changeCursorState();
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void openDoorServerRpc()
    {
        openDoor();
    }
    private void openDoor()
    {
        IsOpen = true;
        //ThisTrigger.SetActive(false);
        playDoorSoundClientRpc();
        Action = false;
    }

    [ClientRpc]
    private void playDoorSoundClientRpc()
    {

    }

    public void Switch()
    {
        switched = true;
        camera_Logic.changeCursorState();
    }
}
