using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    [SerializeField] private Minimap minimap;

    private void OnTriggerEnter(Collider other)
    {
        if (minimap == null) return;
        if (other.tag == "Enemy")
        {
            minimap.AddEnemy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (minimap == null) return;
        if (other.tag == "Enemy")
        {
            minimap.RemoveEnemy(other.gameObject);
        }
    }

    public void InitMinimap(Minimap _minimap)
    {
        minimap = _minimap;
    }
}
