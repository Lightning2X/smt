using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class OpenDoor : MonoBehaviour
{
    public GameObject AnimeObject;
    public GameObject ThisTrigger;
    public AudioSource DoorOpenSound;
    public bool Action = false;
    public bool IsOpen = false;
 

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Action = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (IsOpen == true)
        {
            AnimeObject.GetComponent<Animator>().Play("door_2_close");
            DoorOpenSound.Play();
            IsOpen = false;
        }
        Action = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                AnimeObject.GetComponent<Animator>().Play("door_2_open");
                IsOpen = true; 
                //ThisTrigger.SetActive(false);
                DoorOpenSound.Play();
                Action = false;
            }
        }
    }
}


