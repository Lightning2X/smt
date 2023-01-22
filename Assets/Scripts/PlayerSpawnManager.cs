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
    private Character localPlayerCharacter = Character.Null;
    public override void OnNetworkSpawn()
    {
        //if (!IsOwner) return;
        Debug.Log("spawned" + NetworkManager.Singleton.LocalClientId);
        localPlayerCharacter = Character.Null;

        //change when menu works
        if (IsHost) localPlayerCharacter = Character.Sivion;
        else localPlayerCharacter = Character.Donus;

        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, localPlayerCharacter);
    }
    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ulong clientId, Character spawnId)
    {
        GameObject newPlayer;
        Vector3 spawnPos = spawnPoints.GetChild((int)spawnId).position;
        Quaternion spawnRot = spawnPoints.GetChild((int)spawnId).rotation;
        if (spawnId == 0)
            newPlayer = Instantiate(playerSivion,spawnPos,spawnRot);
        else
            newPlayer = Instantiate(playerDonus, spawnPos, spawnRot);
        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);       
        netObj.SpawnAsPlayerObject(clientId, true);
    }

    public Character GetLocalCharacter { get { return localPlayerCharacter; } }
}
