using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //Array to hold card sprites
    public Sprite[] cardSprites;

    //Array to hold card values;
    int[] cardValues = new int[53];

    void Start()
    {
        
    }

    //swap card sprites and corresponding card values at random to shuffle deck
    public void Shuffle()
    {
        //swap cards random number of times
        //minimum 500 to help randomization.
        int swaps = UnityEngine.Random.Range(500, 1000);

        for(int i = 0 ; i < swaps; i++) 
        {
            //Generate random cards to swap
            int card_A = UnityEngine.Random.Range(1, 52);
            int card_B = UnityEngine.Random.Range(1, 52);

            //Swap Sprites
            Sprite tempSprite = cardSprites[card_A];
            cardSprites[card_A] = cardSprites[card_B];
            cardSprites[card_B] = tempSprite;

            //Swap Corresponding Values
            int tempValue = cardValues[card_A];
            cardValues[card_A] = cardValues[card_B];
            cardValues[card_B] = tempValue;
        }
    }

    public int Deal()
    {
        return 1;
    }

    //method created to assist card class in getting card back from array
    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
    

    //Hard Coded Values to match images of card sprites in cardSprites Array.
    //Suits order is Club(1-13), Diamonds(14-26), Hearts(27-39), Spades(4-52).
    //Loop can be created later if neccesary to apply values, but since values
    //will never change, considered it unnecessary. 
    void AssignCardValues()
    {
        cardValues[1] = 1;
        cardValues[2] = 2;
        cardValues[3] = 3;
        cardValues[4] = 4;
        cardValues[5] = 5;
        cardValues[6] = 6;
        cardValues[7] = 7;
        cardValues[8] = 8;
        cardValues[9] = 9;
        cardValues[10] = 10;
        cardValues[11] = 10;
        cardValues[12] = 10;
        cardValues[13] = 10;
        cardValues[14] = 1;
        cardValues[15] = 2;
        cardValues[16] = 3;
        cardValues[17] = 4;
        cardValues[18] = 5;
        cardValues[19] = 6;
        cardValues[20] = 7;
        cardValues[21] = 8;
        cardValues[22] = 9;
        cardValues[23] = 10;
        cardValues[24] = 10;
        cardValues[25] = 10;
        cardValues[26] = 10;
        cardValues[27] = 1;
        cardValues[28] = 2;
        cardValues[29] = 3;
        cardValues[30] = 4;
        cardValues[31] = 5;
        cardValues[32] = 6;
        cardValues[33] = 7;
        cardValues[34] = 8;
        cardValues[35] = 9;
        cardValues[36] = 10;
        cardValues[37] = 10;
        cardValues[38] = 10;
        cardValues[39] = 10;
        cardValues[40] = 1;
        cardValues[41] = 2;
        cardValues[42] = 3;
        cardValues[43] = 4;
        cardValues[44] = 5;
        cardValues[45] = 6;
        cardValues[46] = 7;
        cardValues[47] = 8;
        cardValues[48] = 9;
        cardValues[49] = 10;
        cardValues[50] = 10;
        cardValues[51] = 10;
        cardValues[52] = 10;

    }
}
