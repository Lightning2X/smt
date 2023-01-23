using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbTrigger : MonoBehaviour
{
    private AudioSource AS;

    void OnTriggerEnter(Collider collision)
    {
        AS = collision.GetComponent<AudioSource>();
        if (AS == null) return;
        if (collision.tag == "Player")
        {
            AS.volume = 1.0f;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        AS = collision.GetComponent<AudioSource>();
        if (AS == null) return;
        AS.volume = 0.2f;
    }

}


