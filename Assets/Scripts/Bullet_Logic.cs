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
    //public override void OnNetworkSpawn()
    //{
    //}

    public void FireBullet(GameObject player)
    {
        playerObj = player;
        m_Rigidbody = GetComponent<Rigidbody>();
        direction = playerObj.transform.rotation.normalized;
        fire = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!fire) return;
        Destroy(gameObject, 2);
        //transform.position += (direction * Vector3.forward).normalized * Time.deltaTime * 2;

    }
    private void FixedUpdate()
    {
        if(!fire) return;
        m_Rigidbody.MovePosition(transform.position + (direction * Vector3.forward).normalized * Time.deltaTime * bullet_Speed);
    }
    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);
    }
}
