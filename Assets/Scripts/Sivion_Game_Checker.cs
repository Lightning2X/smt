using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Sivion_Game_Checker : NetworkBehaviour
{
    public bool slide = false;
    public bool light = false;
    [SerializeField] private GameObject dooropener;

    // Update is called once per frame
    void Update()
    {
        minigameCheck();
    }

    public void minigameCheck()
    {
        if(slide && light)
            dooropener.gameObject.transform.GetComponent<OpenDoor>().access = true;
    }
}
