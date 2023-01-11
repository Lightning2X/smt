using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Minimap : NetworkBehaviour
{
    [SerializeField] private RectTransform playerInMap;
    [SerializeField] private RectTransform map2dEnd;
    //Serialize field only to check
    [SerializeField] private Transform map3dParent;
    [SerializeField] private Transform map3dEnd;

    private Vector3 normalized, mapped;
    public override void OnNetworkSpawn()
    {
        map3dParent = GameObject.Find("Map").transform;
        map3dEnd = GameObject.Find("End").transform;
    }
    private void Update()
    {
        if (!IsOwner || playerInMap == null || gameObject == null) return;
        normalized = Divide(
                map3dParent.InverseTransformPoint(this.transform.position),
                map3dEnd.position - map3dParent.position
            );
        normalized.y = normalized.z;
        mapped = Multiply(normalized, map2dEnd.localPosition);
        mapped.z = 0;
        playerInMap.localPosition = mapped;
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
        if (!IsOwner || gameObject == null || playerInMap == null) return;
        var newRot = transform.eulerAngles;
        newRot.x = 0;
        //newRot.z = 360 - newRot.y;
        newRot.z = 450 - newRot.y;
        newRot.y = 0;
        playerInMap.rotation = Quaternion.Euler(newRot);
    }
}
