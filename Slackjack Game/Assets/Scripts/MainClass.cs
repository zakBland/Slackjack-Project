using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClass : MonoBehaviour
{

    //add arrows to gui
    //add soft hand button options
    //add card total currently
    //change color of name for current player
    //show how softhand is played

    public const int DECK_SIZE = 52;
    public const int SUIT_COUNT = 4;
    public const int PIP_COUNT = 13;
    public const int MAX_PLAYERS = 3;
    public static Player[] players;
    public static List<Card> deck;
    public static int currentPlayerNumber;

    void Start()
    {
        players = new Player[MAX_PLAYERS];
        /*
         players[0].playerNameBlockString = "DealerCardAreaBlock";
         players[1].playerNameBlockString = "PlayerCardAreaBlock";
         players[2].playerNameBlockString = "SamCardAreaBlock";
         players[3].playerNameBlockString = "JillCardAreaBlock";

         players[0].playerNumber = 0;
         players[1].playerNumber = 1;
         players[2].playerNumber = 2;
         players[3].playerNumber = 3;

         players[0].playerName = "Dealer";
         players[1].playerName = "You";
         players[2].playerName = "Sam";
         players[3].playerName = "Jill";
        */

        Debug.Log($"Hiiii");

        currentPlayerNumber = 0;

        //GameFunctionsScript.dealCards();
        deck = GameFunctionsScript.shuffleDeck();
        Debug.Log($"plz");

       // for (int i = 0; i < deck.Count; i++)
        //{
            Debug.Log($"Card is {deck[2].suit}{deck[2].pip}");
        //}

        Debug.Log($"Hiyasssiii");




    }

}

