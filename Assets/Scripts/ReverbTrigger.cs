using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbTrigger : MonoBehaviour
{
    private AudioSource AS;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            AS = collision.GetComponent<AudioSource>();
            AS.volume = 1.0f;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        AS = collision.GetComponent<AudioSource>();
        AS.volume = 0.2f;
    }

}


