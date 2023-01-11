using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Bullet_Logic : NetworkBehaviour
{
    private float bullet_Speed = 6;
    private GameObject playerObj = null;
    private Quaternion direction = Quaternion.identity;
    Rigidbody m_Rigidbody;
    bool fire = false;
    // Start is called before the first frame update

    //void Start() 
    //{
    //    if (playerObj == null)
    //        playerObj = GameObject.FindGameObjectWithTag("Player");

    //    m_Rigidbody = GetComponent<Rigidbody>();

    //    direction = playerObj.transform.rotation.normalized;
    //}
    public override void OnNetworkSpawn()
    {
        fire = true;
        Destroy(gameObject, 2);
    }

    public void FireBullet(GameObject player)
    {
        playerObj = player;
        m_Rigidbody = transform.GetComponent<Rigidbody>();
        direction = playerObj.transform.rotation.normalized;
    }
    private void FixedUpdate()
    {
        if((!IsServer && !fire && !IsSpawned) || m_Rigidbody == null) return;
        m_Rigidbody.MovePosition(transform.position + (direction * Vector3.forward).normalized * Time.deltaTime * bullet_Speed);
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (!IsServer || gameObject == null) return;
        Destroy(gameObject);
    }
}
