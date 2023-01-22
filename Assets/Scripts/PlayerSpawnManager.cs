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

        Debug.Log("localPlayerCharacter: " + localPlayerCharacter);
        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, localPlayerCharacter);
    }
    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ulong clientId, Character spawnId)
    {
        GameObject newPlayer;
        Vector3 spawnPos = spawnPoints.GetChild((int)spawnId).position;
        Quaternion spawnRot = spawnPoints.GetChild((int)spawnId).rotation;
        if (spawnId == Character.Sivion)
            newPlayer = Instantiate(playerSivion,spawnPos,spawnRot);
        else if(spawnId == Character.Donus)
            newPlayer = Instantiate(playerDonus, spawnPos, spawnRot);
        else
            newPlayer = Instantiate(genericPlayer, spawnPoints.position, spawnRot);

        Debug.Log("Clientid: " + clientId + " Pos: " + newPlayer.transform.position);
        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
             
        netObj.SpawnAsPlayerObject(clientId, true);
        newPlayer.SetActive(true);
    }

    public Character GetLocalCharacter { get { return localPlayerCharacter; } }
}
