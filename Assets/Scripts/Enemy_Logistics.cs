using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Enemy_Logistics : NetworkBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform enemyTransform;

    void Update()
    {
        //return if it's not the local player
        EnemyAttack();
    }



    private void EnemyAttack()
    {
        if(Distance(playerTransform, enemyTransform) > 3)
            return;
        //move towards player
        enemyTransform.position += MoveTo(playerTransform.position, enemyTransform.position) * Time.deltaTime;
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
        enemyTransform.position += new Vector3(0,0,5);
    }
}
