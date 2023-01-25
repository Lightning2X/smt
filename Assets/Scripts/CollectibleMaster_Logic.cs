using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CollectibleMaster_Logic : NetworkBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject dooropener;
    private bool finished = false;
    private bool riddled = false;
    public AudioClip[] ac;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.childCount == 0 && !finished)
        {
            finished = true;

            /*
            GameObject mcRiddle = Instantiate(riddle,
            transform.position,
            Quaternion.identity);

            mcRiddle.GetComponent<NetworkObject>().Spawn();

            Destroy(gameObject);*/

            StartCoroutine(PlayLoop()); 
        }
    }

    void Update()
    {
        if(finished)
        {
            if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha4))
            {GetComponent<AudioSource>().clip = ac[1];
                GetComponent<AudioSource>().Play();}
            if (Input.GetKey(KeyCode.Alpha3))
            {
                door.transform.GetComponent<AudioSource>().Play();
                GetComponent<AudioSource>().clip = ac[0];
                GetComponent<AudioSource>().Play();
                riddled = true;
                dooropener.gameObject.transform.GetComponent<OpenDoor>().access = true;
                //Destroy(gameObject);
                //victory sound
            }
        } 
    }

    IEnumerator PlayLoop()
    {
        while(!riddled)
        {
            GetComponent<AudioSource>().clip = ac[2];
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(15);  // delay between loops
        }
    }
}
