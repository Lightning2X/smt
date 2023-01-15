using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class Minimap : NetworkBehaviour
{
    [SerializeField] private RectTransform localPlayerInMap;
    [SerializeField] private RectTransform coopPlayerInMap;
    [SerializeField] private RectTransform map2dEnd;
    
    private Transform map3dParent;
    private Transform map3dEnd;
    [SerializeField] private GameObject minimapCanvas;
    [SerializeField] private RectTransform enemyInMapPrefab;
    private Transform coopPlayerInScene;
    [SerializeField] private List<GameObject> enemiesInScene = new List<GameObject>();
    [SerializeField] private List<RectTransform> enemiesInMap = new List<RectTransform>();
    private Vector3 normalized, mapped;
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) {
            minimapCanvas.SetActive(false);
            return; }
        map3dParent = GameObject.Find("Map").transform;
        map3dEnd = GameObject.Find("End").transform;
        enemiesInScene = new List<GameObject>();
        enemiesInMap = new List<RectTransform>();
        searchForCoopPlayer();
        if(coopPlayerInScene == null) coopPlayerInMap.gameObject.SetActive(false);
    }

    private void searchForEnemies()
    {
        if(enemiesInScene == null)
        {
            enemiesInMap = null;
            enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy").ToList();
            foreach (GameObject enemy in enemiesInScene)
            {
                enemiesInMap.Add(Instantiate(enemyInMapPrefab,minimapCanvas.transform.GetChild(0)));
            }
        }
    }
    //add single enemy to the minimap
    public void AddEnemy(GameObject enemy)
    {
        if (!IsOwner) return;
        Debug.Log("gameObject: " + gameObject + " contains enemy: " + enemiesInScene.Contains(enemy));
        if(!enemiesInScene.Contains(enemy))
        {
            enemiesInScene.Add(enemy);
            enemiesInMap.Add(Instantiate(enemyInMapPrefab, minimapCanvas.transform.GetChild(0)));
            if (coopPlayerInScene != null)
            {
                Debug.Log("this gameObject: " + gameObject);
                Debug.Log("coop: " + coopPlayerInScene.gameObject);
                coopPlayerInScene.GetComponent<Minimap>().AddEnemy(enemy);
            }
        }
    }
    //remove single enemy from the minimap
    public void RemoveEnemy(GameObject enemy)
    {
        if (!IsOwner) return;
        if (enemiesInScene.Contains(enemy))
        {
            //use for loop since enemy gameobject is different than the one in enemiesInMap
            for (int i = 0; i < enemiesInScene.Count; i++)
            {
                if (enemiesInScene[i] == enemy)
                {
                    enemiesInScene.RemoveAt(i);
                    Destroy(enemiesInMap[i].gameObject);
                    enemiesInMap.RemoveAt(i);
                    if (coopPlayerInScene != null) coopPlayerInScene.GetComponent<Minimap>().RemoveEnemy(enemy);
                    break;
                }
            }
        }

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

        if (enemiesInScene != null)
        {
            for (int i = 0; i < enemiesInScene.Count; i++)
            {
                updateMapPos(enemiesInScene[i].transform, enemiesInMap[i]);
            }
        }
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