using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzle_Logic : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera cam;
    private GameObject[] blocks;
    private float swapDistance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        blocks = GameObject.FindGameObjectsWithTag("SlidingPuzzle_Block");
        float height = blocks[1].GetComponent<SpriteRenderer>().bounds.size.x;
        swapDistance = height;
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit)
            {
                double dis = Vector2.Distance(emptySpace.position, hit.transform.position);
                if(dis <= swapDistance)
                {
                    Vector2 lastEmptySpacePos = emptySpace.position;
                    Block_Logic thisBlock = hit.transform.GetComponent<Block_Logic>();
                    emptySpace.position = thisBlock.targetPos;
                    thisBlock.targetPos = lastEmptySpacePos;
                }
            }
            if(Correct()) //should be extended with the wanted functionality for succeding the puzzle
                Debug.Log("Woohoo");
        }
    }

    //shuufles all the blocks on the board except for the empty space
    public void Shuffle()
    {
        for(int n = 1; n < blocks.Length; n++)
        {
            int rnd = Random.Range(1, blocks.Length);
            Vector2 safePos = blocks[n].GetComponent<Block_Logic>().targetPos;
            Vector2 testPos = blocks[rnd].GetComponent<Block_Logic>().targetPos;
            blocks[n].GetComponent<Block_Logic>().targetPos = testPos;
            blocks[rnd].GetComponent<Block_Logic>().targetPos = safePos;
        }
    }

    //Checks if the current board is correct by checking if every piece is in correct position, return true or false accordingly
    public bool Correct()
    {
        for(int n = 1; n < blocks.Length; n++)
        {
            if(blocks[n].GetComponent<Block_Logic>().targetPos == blocks[n].GetComponent<Block_Logic>().correctPos)
                continue;
            return false;
        }
        return true;
    }
}
