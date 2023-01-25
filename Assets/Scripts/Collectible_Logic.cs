using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode;
using UnityEngine;

public class Collectible_Logic : NetworkBehaviour
{
    private float spinning = 50;
    public override void OnNetworkSpawn()
    {
        Debug.Log("hello");
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, spinning * Time.deltaTime, 0); //spins about y-axis as demo
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        Debug.Log("is player: " + other.gameObject.tag);
        other.gameObject.GetComponent<Player_Logic>().AddCollectible();
        gameObject.GetComponent<NetworkObject>().Despawn(true);
    }
}
