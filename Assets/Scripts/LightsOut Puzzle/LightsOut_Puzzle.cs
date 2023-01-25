using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightsOut_Puzzle : MonoBehaviour
{
    // Start is called before the first frame update
    int boardSize = 25;
    int[] masks;
    int playBoard = 0;
    bool finished = false;
    [SerializeField] private Transform BoardButtons;
    void Start()
    {
        masks = new int[boardSize];
        //bit masking, to represent button presses
        for (int i = 0; i < boardSize; i++)
        {
            int board = 0;
            board += 1 << i;
            if ((i + 1) % 5 != 0)
                board += 1 << (i + 1);
            if ((i % 5) != 0)
                board += 1 << (i - 1);
            if ((i + 5) <= 24)
                board += 1 << (i + 5);
            if ((i - 5) > 0)
                board += 1 << (i - 5);
            masks[i] = board;
            /*
                Not sure why this is bugged, but if i is 5 (the first button in the second row) it misses a bit.
                what is expected:
                10000
                11000
                10000
                00000
                what actually happens:
                00000
                11000
                10000
                00000
                the if statement below is a bandaid fix, but everything else works correctly.
            */
            if (i == 5)
                masks[i]++;
            //Debug.Log("i: " + i + " board: " + Convert.ToString(board, toBase: 2));
            int buttonId = i;
            BoardButtons.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate { PressButton(buttonId); });
        }
        CreateRandomBoard();
        UpdateBoard();
    }
    //creates a random board by starting from the complete off state and doing random button presses.
    void CreateRandomBoard()
    {
        playBoard = 0;
        int nrPlays = (int)(boardSize*UnityEngine.Random.Range(0.2f, 0.8f));
        for (int i = 0; i < nrPlays; i++)
        {
            playBoard = playBoard ^ masks[UnityEngine.Random.Range(0, boardSize)];
        }
    }
    //simulates a button press by XOR'ing the mask with the playboard
    public void PressButton(int button)
    {
        if (button >= boardSize)
        {
            Debug.Log("Size not correct: " + button);
            return;
        }           
        playBoard = playBoard ^ masks[button];
        UpdateBoard();
    }
    //Updates the board by reading the bits
    void UpdateBoard()
    {
        if(finished) return;   
        for (int i = 0; i < boardSize; i++)
            {
                Transform button = BoardButtons.GetChild(i);
                if (((playBoard >> i) & 1) == 1)
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "0";
                    button.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "1";
                    button.GetComponent<Image>().color = Color.green;
                }
            }
            if(playBoard == 0)
            {
                finished = true;
                Debug.Log("You Win!");
            }
    }
}
