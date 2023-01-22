using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnManager : NetworkBehaviour
{
    [SerializeField] private GameObject genericPlayer;
    [SerializeField] private GameObject playerSivion;
    [SerializeField] private GameObject playerDonus;
    [SerializeField] private Transform spawnPoints;
    [SerializeField] private GameObject newPlayer;
    private Character localPlayerCharacter = Character.Null;
    public override void OnNetworkSpawn()
    {
        localPlayerCharacter = Character.Null;

        //change when menu works
        if (IsHost) localPlayerCharacter = Character.Sivion;
        else localPlayerCharacter = Character.Donus;

        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId,localPlayerCharacter);
    }
    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ulong clientId, Character spawnId)
    {
        //GameObject newPlayer;
        Vector3 spawnPos = spawnPoints.GetChild((int)spawnId).position;
        Quaternion spawnRot = spawnPoints.GetChild((int)spawnId).rotation;
        if (spawnId == Character.Sivion)
            newPlayer = Instantiate(playerSivion,spawnPos,spawnRot);
        else if(spawnId == Character.Donus)
            newPlayer = Instantiate(playerDonus, spawnPos, spawnRot);
        else
            newPlayer = Instantiate(genericPlayer, spawnPoints.position, spawnRot);

        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
        testClientRpc();
    }
    [ClientRpc]
    private void testClientRpc()
    {
        GameObject.Find("LocalPlayer").GetComponent<Minimap>().setSpawnLocation();
    }
    //private void Test()
    //{
    //    Debug.Log("Test");
    //    if (NetworkManager.Singleton == null) { return; }
    //    Debug.Log("After test");
    //    NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
    //}

    //private void HandleClientConnected(ulong clientId)
    //{
    //    Debug.Log(clientId);
    //    if (clientId == NetworkManager.Singleton.LocalClientId)
    //    {
    //        Debug.Log(clientId + " true " + localPlayerCharacter + " pos " + spawnPoints.GetChild((int)localPlayerCharacter).position);
    //        SpawnPlayerServerRpc(clientId, localPlayerCharacter);
    //    }
    //}
    public Character GetLocalCharacter { get { return localPlayerCharacter; } }
}
