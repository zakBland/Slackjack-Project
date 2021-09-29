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

    public const int DECK_SIZE = 52; //constant max card variables
    public const int SUIT_COUNT = 4; //number of suits
    public const int PIP_COUNT = 13; //number of pips
    public const int MAX_PLAYERS = 4; //max amount of players
    public static Player[] players; //array of all players
    public static List<Card> deck; //deck of cards list varaible
    public static int currentPlayerNumber; //a variable that represents which player's turn it is

    void Start()
    {
        players = new Player[MAX_PLAYERS]; //initializes the player array with max players; maybe initialize with only amount of actual players
        deck = new List<Card>(); //initialize deck array

        //initializes player array's player objects
        for(int i = 0; i < players.Length; i++)
        {
            players[i] = new Player();
        }

        //initializes the name area of each player
        players[0].playerNameBlockString = "DealerCardAreaBlock";
        players[1].playerNameBlockString = "PlayerCardAreaBlock";
        players[2].playerNameBlockString = "SamCardAreaBlock";
        players[3].playerNameBlockString = "JillCardAreaBlock";

        //initializes the player turn variable
        players[0].playerNumber = 0;
        players[1].playerNumber = 1;
        players[2].playerNumber = 2;
        players[3].playerNumber = 3;

        //initializes player name
        players[0].playerName = "Dealer";
        players[1].playerName = "Player";
        players[2].playerName = "Sam";
        players[3].playerName = "Jill";

        //sets current player number/turn to 0;
        currentPlayerNumber = 1;
    }

}

