using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySensor : NetworkBehaviour
{
    [SerializeField] private Minimap minimap;

    private void OnTriggerEnter(Collider other)
    {
        //if (!IsOwner) return;
        if(other.tag == "Enemy")
        {
            minimap.AddEnemy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (!IsOwner) return;
        if (other.tag == "Enemy")
        {
            minimap.RemoveEnemy(other.gameObject);
        }
    }
}
