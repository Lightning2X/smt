using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block_Logic : MonoBehaviour
{
    public RectTransform rectTransform;
    public Vector3 targetPos;
    public Vector3 correctPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = rectTransform.localPosition;
        correctPos = rectTransform.localPosition;
        gameObject.tag = "SlidingPuzzle_Block";
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, targetPos, 0.1f);
    }
}
