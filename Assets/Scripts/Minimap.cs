using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Minimap : NetworkBehaviour
{
    [SerializeField] private RectTransform localPlayerInMap;
    [SerializeField] private RectTransform coopPlayerInMap;
    [SerializeField] private RectTransform map2dEnd;
    //Serialize field only to check
    [SerializeField] private Transform map3dParent;
    [SerializeField] private Transform map3dEnd;
    [SerializeField] private GameObject minimapCanvas;
    private Transform coopPlayerInScene;

    private Vector3 normalized, mapped;
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) {
            minimapCanvas.SetActive(false);
            return; }
        map3dParent = GameObject.Find("Map").transform;
        map3dEnd = GameObject.Find("End").transform;
        searchForCoopPlayer();
        if(coopPlayerInScene == null) coopPlayerInMap.gameObject.SetActive(false);
    }
    private void searchForCoopPlayer()
    {
        if (!IsOwner) return;
        //only check the server, since the player count is always > 1 if a client connects in our case.
        if(IsServer && NetworkManager.Singleton.ConnectedClients.Count < 1)
        {
            return;
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != gameObject)
            {
                coopPlayerInScene = players[i].transform;
                coopPlayerInMap.gameObject.SetActive(true);
                break;
            }
        }
    }
    private void Update()
    {
        if (!IsOwner || gameObject == null) return;
        updateMapPos(transform, localPlayerInMap);

        // search for coop players
        if (coopPlayerInScene == null) searchForCoopPlayer();
        // update if there are. No if else statements, since we can find a coop player and immediately update their pos.
        if (coopPlayerInScene != null) updateMapPos(coopPlayerInScene, coopPlayerInMap);
    }
    private void updateMapPos(Transform playerTransform, RectTransform mapPos)
    {
        normalized = Divide( map3dParent.InverseTransformPoint(playerTransform.position),
                             map3dEnd.position - map3dParent.position);

        normalized.y = normalized.z;
        mapped = Multiply(normalized, map2dEnd.localPosition);
        mapped.z = 0;
        mapPos.localPosition = mapped;
    }
    private void updateMapRot(Transform playerTransform, RectTransform mapRot)
    {
        var newRot = playerTransform.eulerAngles;
        newRot.x = 0;
        //newRot.z = 360 - newRot.y;
        newRot.z = 450 - newRot.y;
        newRot.y = 0;
        mapRot.rotation = Quaternion.Euler(newRot);
    }
    private static Vector3 Divide(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    private static Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    void LateUpdate()
    {
        if (!IsOwner || gameObject == null) return;
        updateMapRot(transform, localPlayerInMap);
        if (coopPlayerInScene != null) updateMapRot(coopPlayerInScene, coopPlayerInMap);
    }
}
