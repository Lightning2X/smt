using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class OpenDoor : NetworkBehaviour
{
    public GameObject AnimeObject;
    public GameObject ThisTrigger;
    public AudioSource DoorOpenSound;
    public bool Action = false;
    public bool IsOpen = false;
    public bool access = false;
    Animator anim;

    public override void OnNetworkSpawn()
    {
        anim = AnimeObject.GetComponent<Animator>();
        anim.SetBool("character_nearby", false);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Action = true;
            //Debug.Log(collision.gameObject);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (IsOpen == true)
        {
            //AnimeObject.GetComponent<Animator>().Play("door_2_close");
            anim.SetBool("character_nearby", false);
            DoorOpenSound.Play();
            IsOpen = false;
        }
        Action = false;
    }


    void Update()
    {
        if (access)
        { 
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(Action)
                {
                    if (IsHost) openDoor();
                    else openDoorServerRpc();
                }
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
        //AnimeObject.GetComponent<Animator>().Play("door_2_open");
        anim.SetBool("character_nearby", true);
        IsOpen = true;
        //ThisTrigger.SetActive(false);
        playDoorSoundClientRpc();
        Action = false;
    }

    [ClientRpc]
    private void playDoorSoundClientRpc()
    {
        DoorOpenSound.Play();
    }
}


