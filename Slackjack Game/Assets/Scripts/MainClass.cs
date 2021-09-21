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
    public const int MAX_PLAYERS = 4;
    public static Player[] players;
    public static List<Card> deck;
    public static int currentPlayerNumber;

    void Start()
    {
        players = new Player[MAX_PLAYERS];
        deck = new List<Card>();
        for(int i = 0; i < players.Length; i++)
        {
            players[i] = new Player();
        }

        for (int i = 0; i < 52; i++)
        {
            Debug.Log($"{deck[i].pip}");
        }
        deck = GameFunctionsScript.shuffleDeck(deck);
        
         players[0].playerNameBlockString = "DealerCardAreaBlock";
         players[1].playerNameBlockString = "PlayerCardAreaBlock";
         players[2].playerNameBlockString = "SamCardAreaBlock";
         players[3].playerNameBlockString = "JillCardAreaBlock";

        //helpButtonObject.GetComponent<Button>();
         players[0].playerNumber = 0;
         players[1].playerNumber = 1;
         players[2].playerNumber = 2;
         players[3].playerNumber = 3;

         players[0].playerName = "Dealer";
         players[1].playerName = "You";
         players[2].playerName = "Sam";
         players[3].playerName = "Jill";
       


        Debug.Log($"Hiiii");

        currentPlayerNumber = 0;

        //GameFunctionsScript.dealCards(deck, players);
        Debug.Log($"plz");

        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.Log($"Card is Player{j}'s: {players[j].playerHand[i].suit}{players[j].playerHand[i].pip}");
            }
        }
        Debug.Log($"Hiyasssiii");




    }

}

