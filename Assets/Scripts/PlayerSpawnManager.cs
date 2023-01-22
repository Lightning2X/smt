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
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        int spawnId = 2;
        if (IsHost) spawnId = 0;
        else spawnId = 1;

        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, spawnId);
    }
    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ulong clientId, int spawnId)
    {
        GameObject newPlayer;
        Vector3 spawnPos = spawnPoints.GetChild(spawnId).position;
        Quaternion spawnRot = spawnPoints.GetChild(spawnId).rotation;
        if (spawnId == 0)
            newPlayer = Instantiate(playerSivion,spawnPos,spawnRot);
        else
            newPlayer = Instantiate(playerDonus, spawnPos, spawnRot);
        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);       
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}
