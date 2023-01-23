using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CollectibleMaster_Logic : NetworkBehaviour
{
    [SerializeField] private GameObject riddle;

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0)
        {
            GameObject mcRiddle = Instantiate(riddle,
            transform.position,
            Quaternion.identity);

            mcRiddle.GetComponent<NetworkObject>().Spawn();

            Destroy(gameObject);
        }
    }
}
