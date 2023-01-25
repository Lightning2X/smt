using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidePuzzle_Logic : MonoBehaviour
{
    //[SerializeField] private 
    private RectTransform emptySpace = null;
    private Image emptyImage = null;
    [SerializeField] private GameObject emptyBlock = null;
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private GameObject mgc;
    //private GameObject[] blocks;
    private float swapDistance = 45f;
    private int inversions;
    private int[] location;
    private bool completed = false;
    // Start is called before the first frame update
    void Start()
    {
        emptyImage = emptyBlock.transform.GetComponent<Image>();
        emptySpace = emptyBlock.transform.GetComponent<RectTransform>();
        location = new int[blocks.Length];
        for(int x = 0; x < location.Length; x++)
            {location[x] = x;}
        Invoke("Shuffle", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TileMove(GameObject block)
    {
        if(completed) return;

        Block_Logic thisBlock = block.transform.GetComponent<Block_Logic>();
        double dis = Vector2.Distance(emptySpace.localPosition, thisBlock.targetPos);
        if(dis <= swapDistance)
        {
            Vector2 lastEmptySpacePos = emptySpace.localPosition;
            emptySpace.localPosition = thisBlock.targetPos;
            thisBlock.targetPos = lastEmptySpacePos;
        }

        if(Correct()) //should be extended with the wanted functionality for succeding the puzzle
            {
                mgc.gameObject.transform.GetComponent<Sivion_Game_Checker>().slide = true;
                completed = true;
                emptyImage.enabled = true;
            }

    }

    //shuffles all the blocks on the board except for the empty space
    public void Shuffle()
    {
        for(int n = 0; n < blocks.Length; n++)
        {
            int rnd = Random.Range(0, blocks.Length);
            Vector3 safePos = blocks[n].GetComponent<Block_Logic>().targetPos;
            Vector3 testPos = blocks[rnd].GetComponent<Block_Logic>().targetPos;
            blocks[n].GetComponent<Block_Logic>().targetPos = testPos;
            blocks[rnd].GetComponent<Block_Logic>().targetPos = safePos;
            int holder = location[n];
            location[n] = location[rnd];
            location[rnd] = holder;
        }

        if(CheckInversions() % 2 != 0) Shuffle();
        
        swapDistance = Vector2.Distance(emptySpace.localPosition, blocks[blocks.Length -1].GetComponent<Block_Logic>().correctPos) * 1.1f;
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
    
    //function to check the number of inversions
    public int CheckInversions()
    {
        inversions = 0;
        for (int i = 0; i < location.Length; i++)
        {
            for (int j = i + 1; j < location.Length; j++)
            {
                if (location[i] > location[j])
                {
                    inversions++;
                }
            }
        }
        return inversions;
    }
}
