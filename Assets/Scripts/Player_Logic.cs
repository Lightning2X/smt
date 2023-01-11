using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player_Logic : NetworkBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private GameObject bullet;
    private Vector3 localPlayerVelocity;
    private float movementSpeed = 3;
    private string playerName = "LocalPlayer";
    private Camera_Logic cam;
    private NetworkVariable<int> collectibles = new NetworkVariable<int>(0);
    bool cursorLocked = false;
    private float shootCD = 1;
    private float lastShot = 0;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        gameObject.name = playerName;
        //gameObject.tag = playerName;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Logic>();
        cam.InitLocalPlayer(gameObject.transform);
    }

    void Update()
    {
        //Debug.Log(OwnerClientId + " collectibles: " + collectibles.Value);
        if (!IsOwner) return;
        PlayerShoot();
    }
    void FixedUpdate()
    {
        //return if it's not the local player
        if (!IsOwner) return;
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        //reset velocity dir
        localPlayerVelocity = Vector3.zero;
        //We could make configurable keys. use forward etc for when you move around the camera
        if (Input.GetKey(KeyCode.W)) localPlayerVelocity += transform.forward;
        if (Input.GetKey(KeyCode.A)) localPlayerVelocity -= transform.right;
        if (Input.GetKey(KeyCode.S)) localPlayerVelocity -= transform.forward;
        if (Input.GetKey(KeyCode.D)) localPlayerVelocity += transform.right;

        localPlayerVelocity.y = 0;
        transform.position += localPlayerVelocity.normalized * movementSpeed * Time.deltaTime;
    }

    public void AddCollectible()
    {
        collectibles.Value++;
    }
    private void PlayerShoot()
    {
        if (Time.time - lastShot < shootCD)
            return;

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (NetworkManager.Singleton.IsServer)
                fireBullet();
            else
                fireBulletServerRpc();
            lastShot = Time.time;
        }
    }
    private void fireBullet()
    {
        GameObject fireBullet = Instantiate(bullet,
            transform.position + (transform.forward.normalized),
            Quaternion.identity);
        fireBullet.GetComponent<NetworkObject>().Spawn();
        fireBullet.GetComponent<Bullet_Logic>().FireBullet(transform.gameObject);
    }
    [ServerRpc]
    private void fireBulletServerRpc()
    {
        fireBullet();
    }
}
