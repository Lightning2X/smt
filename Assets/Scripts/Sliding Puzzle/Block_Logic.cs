using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Logic : MonoBehaviour
{
    public Vector3 targetPos;
    public Vector3 correctPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        correctPos = transform.position;
        gameObject.tag = "SlidingPuzzle_Block";
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
    }
}
