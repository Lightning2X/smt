using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CollectibleMaster_Logic : NetworkBehaviour
{
    //[SerializeField] private GameObject riddle;
    private bool finished = false;

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
            {/*spawn enemies, suspend riddle*/}
            if (Input.GetKey(KeyCode.Alpha3))
            {
                Destroy(gameObject);
                //victory sound
            }
        } 
    }

    IEnumerator PlayLoop()
    {
        while(true)
        {
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(15);  // delay between loops
        }
    }
}
