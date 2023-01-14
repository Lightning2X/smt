using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Enemy_Logistics : NetworkBehaviour
{
    //[SerializeField] private Transform playerTransform;
    private Transform playerTransform;
    private GameObject[] playerObj = null;
    private float minDistance = 6;

    //[SerializeField] private Transform enemyTransform;
    [SerializeField] private AudioClip clip;

    private AudioSource audioSource;

    public override void OnNetworkSpawn()
    {
        audioSource = GetComponent<AudioSource>();

        if (playerObj == null)
            playerObj = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        if (playerObj == null) return;
        EnemyAttack();
    }

    private void EnemyAttack()
    {
        GameObject closestPlayer = null;
        float closest = minDistance;

        for(int x = 0; x < playerObj.Length; x++)
        {
            float dis = Distance(playerObj[x].transform, transform);

            if(dis > closest) continue;

            closestPlayer = playerObj[x];
            closest = minDistance;
        }

        if(closestPlayer == null) return;

        //move towards player
        if(!audioSource.isPlaying) audioSource.Play();

        transform.position += MoveTo(closestPlayer.transform.position, transform.position) * Time.deltaTime;
    }

    private float Distance(Transform ob1, Transform ob2)
    {
        return Vector3.Distance(ob1.position, ob2.position);
    }

    private Vector3 MoveTo(Vector3 ob1, Vector3 ob2)
    {
        return (ob1 - ob2).normalized;
    }

   private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "EnemySensor") return;
        transform.position += new Vector3(0,0,5);
    }
}
